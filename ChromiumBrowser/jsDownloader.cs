using KitsuneBrowser.Properties;
using System;


namespace KitsuneBrowser
{
    public class jsDownloader
    {



        public string MsgBox(string alert)
        {
            return Resources.messageBox.Replace("$messageBox-placeholder$",alert);
        }
        public void PauseResume(string id)
        {
            DownloadObject obj = BrowserChromium.instance.downloadManager.getItem(id);
            if (obj != null)
            {
                if (obj.status == DownloadStatus.Paused)
                {
                    obj.status = DownloadStatus.Progress;
                    obj.callback.Resume();
                }
                else if (obj.status == DownloadStatus.Progress)
                {
                    obj.status = DownloadStatus.Paused;
                }
            }
        }
        public void CancelDownload(string id)
        {
            DownloadObject obj = BrowserChromium.instance.downloadManager.getItem(id);
            if (obj != null)
            {
                obj.status = DownloadStatus.Canceled;
                obj.callback.Cancel();
            }
        }
    }
}
