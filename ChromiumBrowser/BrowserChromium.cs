using CefSharp;
using CefSharp.WinForms;
using KitsuneBrowser.Properties;
using EasyTabs;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using KitsuneBrowser.Controls;
using System.Drawing;
using System.Runtime.InteropServices;
using Win32Interop.Enums;
using Win32Interop.Methods;
using Win32Interop.Structs;
using AutoUpdaterDotNET;

namespace KitsuneBrowser
{
    public partial class BrowserChromium : TitleBarTabs
    {
        public static BrowserChromium instance;
        public List<string> mainConsole = new List<string>();
        public DownloadManager downloadManager = new DownloadManager();
        public Settings settings;
        public Dictionary<string, Icon> favicons_loaded = new Dictionary<string, Icon>();
        public BrowserChromium()
        {
        //    this.Cursor = new Cursor(Cursor.Current.Handle);
            instance = this;
            if (BrowserChromium.instance.settings == null)
            {
                BrowserChromium.instance.settings = new Settings();
            }
            InitializeComponent();
            // Enable or disable viewing tabs through the taskbar
            AeroPeekEnabled = true;
            // Set the tab rendering engine that you wish to use
            TabRenderer = new BrowserTabRenderer(this);
         
            // this.tab
            Text = "Kitsune Browser";
            Icon = Resources.kitsune;
            this.AeroPeekEnabled = false;
            AutoUpdater.Mandatory = true;
            AutoUpdater.Start("https://raw.githubusercontent.com/AlessandroCH2/KitsuneBrowser/master/updater.xml");
            /*  DownloadPanel p = new DownloadPanel();
           //   p.BackColor = System.Drawing.Color.Transparent;

              p.Anchor = AnchorStyles.Top | AnchorStyles.Right ;
              p.Location = new System.Drawing.Point(460, 60);
              p.Parent = this;
              this.Controls.Add(p);*/

        }
        public void consoleString(string str)
        {
            mainConsole.Add(str);
        }
        protected void OnTabClicked(TitleBarTabEventArgs e)
        {
            this.Text = SelectedTab.Content.Text;
            TabWindow win = (TabWindow)SelectedTab.Content;
            win.showFavorites();
        }
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            
            bool callDwp = true;
            
            switch ((WM)m.Msg)
            {
               
                // When the window is activated, set the size of the non-client area appropriately
                case WM.WM_ACTIVATE:
                    if ((m.WParam.ToInt64() & 0x0000FFFF) != 0)
                    {
                        SetFrameSize();
                        ResizeTabContents();
                        m.Result = IntPtr.Zero;
                    }

                    break;

                case WM.WM_NCHITTEST:
                    // Call the base message handler to see where the user clicked in the window
                    base.WndProc(ref m);
                    
                    HT hitResult = (HT)m.Result.ToInt32();
                   
                    // If they were over the minimize/maximize/close buttons or the system menu, let the message pass
                    if (!(hitResult == HT.HTCLOSE || hitResult == HT.HTMINBUTTON || hitResult == HT.HTMAXBUTTON || hitResult == HT.HTMENU ||
                          hitResult == HT.HTSYSMENU))
                    {
                        m.Result = new IntPtr((int)HitTest(m));
                    }
                   
                   
                    callDwp = false;

                    break;

                // Catch the case where the user is clicking the minimize button and use this opportunity to update the AeroPeek thumbnail for the current tab
                case WM.WM_NCLBUTTONDOWN:

                  
                    int lParam = (int)m.LParam;
                    Point point = new Point(lParam & 0xffff, lParam >> 16);

                    Point convertedPoint = new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
                    onWindowTopBarClick(convertedPoint);
                    if (((HT)m.WParam.ToInt32()) == HT.HTMINBUTTON && AeroPeekEnabled && SelectedTab != null)
                    {
                        UpdateTabThumbnail(SelectedTab);
                    }

                    break;
                
            }
           
            if (callDwp)
            {
                base.WndProc(ref m);
            }
        }

        public void onWindowTopBarClick(Point point)
        {
            BrowserTabRenderer renderer = (BrowserTabRenderer)this.TabRenderer;
            if (renderer._windowsSizingBoxes._githubButtonArea.Contains(point))
            {

       
                openLinkNewTab("https://github.com/AlessandroCH2/KitsuneBrowser/tree/master");
            }
        }
        private HT HitTest(Message m)
        {
            // Get the point that the user clicked
            int lParam = (int)m.LParam;
            Point point = new Point(lParam & 0xffff, lParam >> 16);

            return HitTest(point, m.HWnd);
        }

        /// <summary>Called when a <see cref="WM.WM_NCHITTEST" /> message is received to see where in the non-client area the user clicked.</summary>
        /// <param name="point">Screen location that we are to test.</param>
        /// <param name="windowHandle">Handle to the window for which we are performing the test.</param>
        /// <returns>One of the <see cref="HT" /> values, depending on where the user clicked.</returns>
        private HT HitTest(Point point, IntPtr windowHandle)
        {
            RECT rect;

            User32.GetWindowRect(windowHandle, out rect);
            Rectangle area = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

            int row = 1;
            int column = 1;
            bool onResizeBorder = false;

            // Determine if we are on the top or bottom border
            if (point.Y >= area.Top && point.Y < area.Top + SystemInformation.VerticalResizeBorderThickness + _nonClientAreaHeight - 2)
            {
                onResizeBorder = point.Y < (area.Top + SystemInformation.VerticalResizeBorderThickness);
                row = 0;
            }

            else if (point.Y < area.Bottom && point.Y > area.Bottom - SystemInformation.VerticalResizeBorderThickness)
            {
                row = 2;
            }

            // Determine if we are on the left border or the right border
            if (point.X >= area.Left && point.X < area.Left + SystemInformation.HorizontalResizeBorderThickness)
            {
                column = 0;
            }

            else if (point.X < area.Right && point.X >= area.Right - SystemInformation.HorizontalResizeBorderThickness)
            {
                column = 2;
            }

            HT[,] hitTests =
            {
                {
                    onResizeBorder
                        ? HT.HTTOPLEFT
                        : HT.HTLEFT,
                    onResizeBorder
                        ? HT.HTTOP
                        : HT.HTCAPTION,
                    onResizeBorder
                        ? HT.HTTOPRIGHT
                        : HT.HTRIGHT
                },
                {
                    HT.HTLEFT, HT.HTNOWHERE, HT.HTRIGHT
                },
                {
                    HT.HTBOTTOMLEFT, HT.HTBOTTOM,
                    HT.HTBOTTOMRIGHT
                }
            };

            return hitTests[row, column];
        }
        static BrowserChromium()
        {
            // This is only so that generating a thumbnail for Aero peek works properly:  with GPU acceleration enabled, all you get is a black box
            // when you try to "snapshot" the web browser control.  If you don't plan on using Aero peek, remove this method.
            CefSettings cefSettings = new CefSettings();
            //  cefSettings.DisableGpuAcceleration();
            cefSettings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 CefSharp Browser/" + Cef.CefSharpVersion; ;
            cefSettings.PersistSessionCookies = true;
            cefSettings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Browser"; 
            cefSettings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = CustomProtocolSchemeHandlerFactory.SchemeName,
                SchemeHandlerFactory = new CustomProtocolSchemeHandlerFactory()
            });
            Cef.Initialize(cefSettings);
        }

        public override TitleBarTab CreateTab()
        {
            return new TitleBarTab(this)
            {
                Content = new TabWindow
                {
                    Text = "New Tab"
                }
            };
        }
        public TitleBarTab CreateTab(string link)
        {
            return new TitleBarTab(this)
            {
                Content = new TabWindow(link)
                {
                    Text = "New Tab",
                   
                }
            };
        }
        public void openLinkNewTab(string linkUrl)
        {
            this.Invoke(new Action(() =>
            {
                TitleBarTab tab = CreateTab(linkUrl);
                this.Tabs.Add(tab);
                ResizeTabContents(tab);

                SelectedTabIndex = _tabs.Count - 1;
                this.RedrawTabs();
                this.Refresh();
             
            }));
          
          

        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        private static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return DwmSetWindowAttribute(handle, (int)attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        private static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
    }
}
