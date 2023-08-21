using CefSharp;
using CefSharp.Enums;
using CefSharp.Structs;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32Interop.Structs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KitsuneBrowser
{

    internal class MenuHandler : IContextMenuHandler
    {
        private const int ShowDevTools = 26501;
        private const int CloseDevTools = 26502;
        private const int SaveImageAs = 26503;
        private const int SaveAsPdf = 26504;
        private const int SaveLinkAs = 26505;
        private const int CopyLinkAddress = 26506;
        private const int OpenLinkInNewTab = 26507;
        private const int CopyImage = 26508;
        private const int CopyImageLink = 26509;
        private const int OpenImageInNewTab = 26510;
        private const int ShowPageSource = 26511;
        public string getTranslated(string key)
        {
            return BrowserChromium.instance.settings.getTranslated(key);
        }
        void IContextMenuHandler.OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
          
            //To disable the menu then call clear
             model.Clear();

            //Removing existing menu item
            //bool removed = model.Remove(CefMenuCommand.ViewSource); // Remove "View Source" option

            //Add new custom menu items
            if (parameters.LinkUrl != "")
            {
                
                model.AddItem((CefMenuCommand)OpenLinkInNewTab, getTranslated("contextmenu_openlinknewtab"));
                model.AddItem((CefMenuCommand)CopyLinkAddress, getTranslated("contextmenu_copylink"));
                model.AddItem((CefMenuCommand)SaveLinkAs, getTranslated("contextmenu_savelinkas"));
                model.AddSeparator();
                if (parameters.HasImageContents && parameters.SourceUrl != "")
                {
                   
                    model.AddItem((CefMenuCommand)OpenImageInNewTab, getTranslated("contextmenu_openimagenewtab"));
                    model.AddItem((CefMenuCommand)SaveImageAs, getTranslated("contextmenu_saveimageas"));
                    model.AddItem((CefMenuCommand)CopyImage, getTranslated("contextmenu_copyimage"));
                    model.AddItem((CefMenuCommand)CopyImageLink, getTranslated("contextmenu_copyimagelink"));
                    // RIGHT CLICKED ON IMAGE
                    model.AddSeparator();
                }
            }
            else
            {
               
                model.AddItem(CefMenuCommand.Back, getTranslated("contextmenu_back")); if (!browser.CanGoBack) model.SetEnabled(CefMenuCommand.Back, false);
                model.AddItem(CefMenuCommand.Forward, getTranslated("contextmenu_forward")); if (!browser.CanGoForward) model.SetEnabled(CefMenuCommand.Forward, false);
                model.AddItem(CefMenuCommand.Reload, getTranslated("contextmenu_reload"));
                model.SetAccelerator(CefMenuCommand.Reload, (int)Keys.R, false, true, false);
                model.SetAccelerator(CefMenuCommand.Back, (int)Keys.Left, false, false, true);
                model.SetAccelerator(CefMenuCommand.Forward, (int)Keys.Right, false, false, true);
                model.AddSeparator();
                model.AddItem(CefMenuCommand.Print, getTranslated("contextmenu_print"));

               
                if (parameters.SelectionText != null)
                {
                   if(parameters.SelectionText.Length > 0)
                    {
                        if (parameters.IsEditable)
                        {
                            model.AddItem(CefMenuCommand.Cut, getTranslated("contextmenu_cut"));
                            model.SetAccelerator(CefMenuCommand.Cut, (int)Keys.X, false, true, false);
                        }
                      
                        model.AddItem(CefMenuCommand.Copy, getTranslated("contextmenu_copy"));
                        model.SetAccelerator(CefMenuCommand.Copy, (int)Keys.C, false, true, false);
                    }

                }
                if (parameters.IsEditable)
                {
                    model.AddItem(CefMenuCommand.Paste, getTranslated("contextmenu_paste"));
                    model.SetAccelerator(CefMenuCommand.Paste, (int)Keys.V, false, true, false);
                }
                model.AddSeparator();
            }

            model.AddItem((CefMenuCommand)ShowPageSource, getTranslated("contextmenu_pagesource"));
            model.AddItem((CefMenuCommand)ShowDevTools, getTranslated("contextmenu_devtools"));
            model.SetAccelerator((CefMenuCommand)ShowPageSource, (int)Keys.U, false, true, false);


        }

        bool IContextMenuHandler.OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            if ((int)commandId == ShowDevTools)
            {
                browser.ShowDevTools();
                return true;
            }
            if ((int)commandId == CloseDevTools)
            {
                browser.CloseDevTools();
                return true;
            }
            if ((int)commandId == SaveImageAs)
            {
                browser.GetHost().StartDownload(parameters.SourceUrl);
                return true;
            }
            if ((int)commandId == SaveLinkAs)
            {
                browser.GetHost().StartDownload(parameters.LinkUrl);
                return true;
            }
            if ((int)commandId == OpenLinkInNewTab)
            {
                BrowserChromium.instance.openLinkNewTab(parameters.LinkUrl);
                return true;
            }
            if ((int)commandId == ShowPageSource)
            {
                BrowserChromium.instance.openLinkNewTab("view-source:"+parameters.PageUrl);
                return true;
            }
            if ((int)commandId == CopyLinkAddress)
            {
                Clipboard.SetText(parameters.LinkUrl);
                return true;
            }
            if ((int)commandId == OpenImageInNewTab)
            {
                BrowserChromium.instance.openLinkNewTab(parameters.SourceUrl);
                return true;
            }
            if ((int)commandId == CopyImage)
            {
                System.Net. WebRequest webreq = System.Net.WebRequest.Create(parameters.SourceUrl);
                System.Net.WebResponse webres = webreq.GetResponse();
                Stream stream = webres.GetResponseStream();

                Image image = Image.FromStream(stream);
                Clipboard.SetImage(image);
                return true;
            }
            if ((int)commandId == CopyImageLink)
            {
                Clipboard.SetText(parameters.SourceUrl);
                return true;
            }
            /* if ((int)commandId == Reload)
             {

                 browser.Reload();
             }
             if ((int)commandId == Forward)
             {
                 browser.Forward();
             }
             if ((int)commandId == Back)
             {
                 browser.Back();
             }*/
            return false;
        }

        void IContextMenuHandler.OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }

        bool IContextMenuHandler.RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }

    public class KeyboardHandler : IKeyboardHandler
    {
        TabWindow linkedWin;

        public KeyboardHandler(TabWindow win)
        {
            linkedWin = win;
        }
        public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            if (type == KeyType.KeyUp && Enum.IsDefined(typeof(Keys), windowsKeyCode))
            {
                var key = (Keys)windowsKeyCode;
                switch (key)
                {
                    case Keys.F5:
                        browser.Reload(true);
                        break;
                    case Keys.F11:
                        if (linkedWin.newScreenMode)
                        {
                            linkedWin.changeFullScreen(false);
                        }
                        else
                        {
                            linkedWin.changeFullScreen(true);
                        }
                        break;
                    case Keys.F12:
                        browser.ShowDevTools();
                        break;
                }
            }
            return false;
        }

        public bool OnPreKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            return false;
        }
    }
    class DisHandler : IDisplayHandler, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Form parent;
        public Form fullScreenForm;

        /// <summary>
        /// For binding to System.Windows.Window.Icon.
        /// </summary>
        TabWindow linkedWin;
        
        public DisHandler(TabWindow win) {
         
            linkedWin = win;
        }





       
        

        public void OnAddressChanged(IWebBrowser chromiumWebBrowser, AddressChangedEventArgs addressChangedArgs)
        {
        }

        public bool OnAutoResize(IWebBrowser chromiumWebBrowser, IBrowser browser, CefSharp.Structs.Size newSize)
        {
            return false;
        }

        public bool OnConsoleMessage(IWebBrowser chromiumWebBrowser, ConsoleMessageEventArgs consoleMessageArgs)
        {
            return false;
        }

        public bool OnCursorChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IntPtr cursor, CursorType type, CursorInfo customCursorInfo)
        {
            return false;
        }

        public void OnFaviconUrlChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IList<string> urls)
        {
            linkedWin.OnFaviconUrlChange(chromiumWebBrowser, browser, urls);
         
        }

        public void OnFullscreenModeChange(IWebBrowser browserControl, IBrowser browser, bool fullscreen)
        {

            linkedWin.changeFullScreen(fullscreen);
        }

        public void OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {

        }

        public void OnStatusMessage(IWebBrowser chromiumWebBrowser, StatusMessageEventArgs statusMessageArgs)
        {
        }

        public void OnTitleChanged(IWebBrowser chromiumWebBrowser, TitleChangedEventArgs titleChangedArgs)
        {
        }

        public bool OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
        {
            return false;
        }
    }
}
