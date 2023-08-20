using CefSharp;
using CefSharp.WinForms;
using KitsuneBrowser.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KitsuneBrowser
{

    public enum DownloadStatus
    {
        Progress,
        Paused,
        Canceled,
        Finished
    }
    public class DownloadObject
    {
        public DownloadItem item;
        public IDownloadItemCallback callback; //Only when a download is not picked from oldest
        public string itemName = "";
       
        public string FileName = "";
        public string dir;
        
        public DownloadStatus status = DownloadStatus.Progress;
        public int PercentComplete = 0;
        public long downloadedBytes;
        public long totalBytes;
        public long downloadSpeed;
        public bool check;
    }
    
    public class DownloadManager : IDownloadHandler
    {

        public event EventHandler<DownloadItem> OnBeforeDownloadFired;

        public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

        public List<DownloadObject> items = new List<DownloadObject>();
        public string Button_Pause = "<svg width=\"20\" height=\"20\" viewBox=\"0 0 16 16\" xmlns=\"http://www.w3.org/2000/svg\">                        <path d=\"M5.5 3.5C6.32843 3.5 7 4.17157 7 5V11C7 11.8284 6.32843 12.5 5.5 12.5C4.67157 12.5 4 11.8284 4 11V5C4 4.17157 4.67157 3.5 5.5 3.5Z\" />                   <path d=\"M10.5 3.5C11.3284 3.5 12 4.17157 12 5V11C12 11.8284 11.3284 12.5 10.5 12.5C9.67157 12.5 9 11.8284 9 11V5C9 4.17157 9.67157 3.5 10.5 3.5Z\"/>                       </svg>";
        public string Button_Resume = "<svg width=\"20\" height=\"20\" viewBox=\"0 0 16 16\" xmlns=\"http://www.w3.org/2000/svg\"><path d=\"M11.5964 8.69663L5.23279 12.3885C4.6925 12.7019 4 12.3228 4 11.6922L4 4.30846C4 3.67783 4.6925 3.29871 5.23279 3.61216L11.5964 7.30403C12.1345 7.6162 12.1345 8.38445 11.5964 8.69663Z\"/></svg>";
        public DownloadManager()
        {
            
        }
        public DownloadObject getItem(string s)
        {
            for(int i=0; i < items.Count; i++)
            {
                DownloadObject item = items[i];
                if (item.itemName == s)
                {
                    return item;
                }
            }
            return null;
        }
        public bool CanDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, string url, string requestMethod)
        {
            if(chromiumWebBrowser == BrowserChromium.instance)
            {
                return true;
            }
            else
            {
                if (browser != null)
                    if (browser.HasDocument == false || browser.IsPopup)
                    {
                        browser.CloseBrowser(true);
                    }
                chromiumWebBrowser.StartDownload(url);
               
                return false;
            }
          
           
        }


        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            OnBeforeDownloadFired?.Invoke(this, downloadItem);

           
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    TabWindow win = (TabWindow)BrowserChromium.instance.SelectedTab.Content;
                    callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                    string script = "document.body.innerHTML += '" + Resources.messageBox + "'; ";
                    win.ExecuteJavascript(script);
                }
            }
        }


        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            OnDownloadUpdatedFired?.Invoke(this, downloadItem);
            var handler = OnDownloadUpdatedFired;
            if (handler != null)
            {
                handler(this, downloadItem);
            }
           
            if (downloadItem.IsValid)
            {
                BrowserChromium.instance.consoleString("(" + downloadItem.FullPath.ToString() + ")" + downloadItem.Url + ": " + downloadItem.PercentComplete+"%");
                DownloadObject obj = getItem("" + downloadItem.Id);
                if (obj != null)
                {
                    if(obj.check == false)
                    {
                        obj.check = true;
                        TabWindow win = (TabWindow)BrowserChromium.instance.SelectedTab.Content;
                        BrowserChromium.instance.consoleString("" + Resources.messageBox);
                        string m = Resources.messageBox;
                        string[] splitUrl = obj.item.Url.Split("/".ToCharArray());
                        string nameSplitted = splitUrl[splitUrl.Length - 1];
                        // string script = "var htmlitem = '<div id=\"BROWSER-MESSAGE-BOX\"style=\"background-color: rgba(59, 64, 84, 0.568);    \r\nborder-radius: 12px;   backdrop-filter: blur(10px);   \r\nwidth: 500px; \r\nheight: 45px;  \r\nfont-family: Arial, Helvetica, sans-serif;\r\nfill: white;\r\ntransition: background-color 0.2s linear;\r\nmargin-top: 10px;    color: #fff;  text-decoration: none; display: flex; align-items: center;\r\n    justify-content: center; position: fixed; bottom: 10px; left: calc(50% - 250px); z-index: 99;\">\r\nDownload started\r\n</div>'; document.body.insertAdjacentHTML('beforeend', htmlitem); ";
                        string script = "document.body.insertAdjacentHTML('beforeend', window.jsDownloader.msgBox('Downloading "+nameSplitted + "')); ";
                           
                        win.ExecuteJavascript(script);
                        script = Resources.messagebox1;

                        win.ExecuteJavascript(script);
                    }
                    if (obj.status == DownloadStatus.Canceled)
                    {
                        callback.Cancel();

                    }
                    if (obj.status == DownloadStatus.Progress)
                    {
                        if (!downloadItem.IsInProgress)
                        {
                            callback.Resume();
                        }

                    }
                    if (obj.status == DownloadStatus.Paused)
                    {
                        if (downloadItem.IsInProgress)
                        {
                            callback.Pause();
                        }

                    }

                    obj.FileName = downloadItem.FullPath;
                    obj.dir = downloadItem.FullPath;
                   
                    obj.totalBytes = downloadItem.TotalBytes;
                    obj.downloadedBytes = downloadItem.ReceivedBytes;
                    obj.downloadSpeed = downloadItem.CurrentSpeed;
                    obj.PercentComplete = downloadItem.PercentComplete;
                    if (downloadItem.IsComplete)
                    {
                        TabWindow win = (TabWindow)BrowserChromium.instance.SelectedTab.Content;
                        obj.status = DownloadStatus.Finished;
                        string m = Resources.messageBox;
                        string[] splitUrl = obj.item.Url.Split("/".ToCharArray());
                        string nameSplitted = splitUrl[splitUrl.Length - 1];
                        // string script = "var htmlitem = '<div id=\"BROWSER-MESSAGE-BOX\"style=\"background-color: rgba(59, 64, 84, 0.568);    \r\nborder-radius: 12px;   backdrop-filter: blur(10px);   \r\nwidth: 500px; \r\nheight: 45px;  \r\nfont-family: Arial, Helvetica, sans-serif;\r\nfill: white;\r\ntransition: background-color 0.2s linear;\r\nmargin-top: 10px;    color: #fff;  text-decoration: none; display: flex; align-items: center;\r\n    justify-content: center; position: fixed; bottom: 10px; left: calc(50% - 250px); z-index: 99;\">\r\nDownload started\r\n</div>'; document.body.insertAdjacentHTML('beforeend', htmlitem); ";
                        string script = "document.body.insertAdjacentHTML('beforeend', window.jsDownloader.msgBox('Download completed " + nameSplitted + "')); ";

                        win.ExecuteJavascript(script);
                        script = Resources.messagebox1;

                        win.ExecuteJavascript(script);
                    }
                }
                else
                {
                    DownloadObject item = new DownloadObject();
                    item.item = downloadItem;
                    item.callback = callback;
                    item.itemName = ""+downloadItem.Id;
                    items.Add(item);
                }


            }

        }
        static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
       public static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value, decimalPlaces); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }
        public static string createDivItem(DownloadObject obj)
        {
            
            int perc =obj.PercentComplete;
            string progressString = perc + "% - " + DownloadManager.SizeSuffix(obj.downloadedBytes) + " / " + DownloadManager.SizeSuffix(obj.totalBytes) + ", " + DownloadManager.SizeSuffix(obj.downloadSpeed) + "/s";
            string str = Resources.download_item.Replace("$itemId$", obj.itemName).Replace("$barSize",""+perc).Replace("$itemName$", obj.FileName).Replace("$linkName$", obj.item.Url);
            // String str = " <div class=\"download-item\" id='ITEM-"+obj.itemName+"'> <div class=\"item-name\">"+obj.FileName + "</div> <div class=\"link-name\">"+obj.item.Url+ "</div> <div class=\"download-progression\" id='progression-ITEM-"+obj.itemName+"'><div class=\"progress-bar-empty\"> <div id='progress-bar-ITEM-"+obj.itemName+"' class=\"progress-bar-filling\" style='width: "+perc+"%;'> </div>   </div> <div class=\"link-name\" style='margin-top:10px;' id='STATUS-ITEM-"+obj.itemName+"'>" + progressString+"</div> </div> </div>";

            return str;
        }
    }
}
