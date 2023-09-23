using CefSharp.WinForms;
using CefSharp;
using System;
using System.Collections.Generic;

using System.Drawing;

using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using KitsuneBrowser.Properties;
using System.Net.Http;

using System.Xml;

using System.Reflection;

using System.Net;

using System.Security.Cryptography.X509Certificates;


namespace KitsuneBrowser
{
   
    public partial class TabWindow : Form
    {
        public CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        public CertificateInfo certificateInfo = null;
        DisHandler handler;
        public string LinkToOpenWith = "kitsune://homepage";
        public TabWindow(string link)
        {
         
            LinkToOpenWith = link;
            creation();
        }
        public void creation()
        {
            InitializeComponent();
            this.chromiumWebBrowser1 = new ChromiumWebBrowser();
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
             | System.Windows.Forms.AnchorStyles.Left)
             | System.Windows.Forms.AnchorStyles.Right)));

            this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 46);

            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(1280, 380);
            this.chromiumWebBrowser1.TabIndex = 0;
            this.chromiumWebBrowser1.TitleChanged += new EventHandler<CefSharp.TitleChangedEventArgs>(this.chromiumWebBrowser1_TitleChanged);
            this.chromiumWebBrowser1.LoadingStateChanged += new EventHandler<CefSharp.LoadingStateChangedEventArgs>(this.chromiumWebBrowser1_LoadingStateChanged);
            this.chromiumWebBrowser1.Paint += new PaintEventHandler(this.chromiumWebBrowser1_Paint);

            this.Controls.Add(this.chromiumWebBrowser1);
            handler = new DisHandler(this);
            chromiumWebBrowser1.DownloadHandler = BrowserChromium.instance.downloadManager;
            chromiumWebBrowser1.MenuHandler = new MenuHandler();
            chromiumWebBrowser1.KeyboardHandler = new KeyboardHandler(this);
            // chromiumWebBrowser1.RegisterJsObject("jsDownloader", new jsDownloader());
            chromiumWebBrowser1.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            chromiumWebBrowser1.JavascriptObjectRepository.Register("jsDownloader", new jsDownloader(), false, options: BindingOptions.DefaultBinder);
            chromiumWebBrowser1.JavascriptObjectRepository.Register("browserSettings", BrowserChromium.instance.settings, false, options: BindingOptions.DefaultBinder);
            /* chromiumWebBrowser1.JavascriptObjectRepository.ResolveObject += (s, e) =>
             {

                     e.ObjectRepository.Register("jsDownloader", new jsDownloader(),true, options: BindingOptions.DefaultBinder);

             };*/
            chromiumWebBrowser1.DisplayHandler = handler;
            favXml = Assembly.GetAssembly(typeof(Settings)).Location.Replace("KitsuneBrowser.exe","") + @"\favorites.xml";
            // chromiumWebBrowser1.LoadUrl("https://google.com");
            parrotToolStrip1.ForeColor = Color.White;
       //     parrotToolStrip1.RenderMode = ToolStripRenderMode.Custom;
            chromiumWebBrowser1.LoadUrl(LinkToOpenWith);
            showFavorites();
            if (BrowserChromium.instance.settings.favorites)
            {
                this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 72);
                

                this.chromiumWebBrowser1.Size = new System.Drawing.Size(this.Width, this.Height - 72);
                
            }
            else
            {
                this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 46);


                this.chromiumWebBrowser1.Size = new System.Drawing.Size(this.Width, this.Height - 46);
            }
           


        }
        public TabWindow()
        {
            LinkToOpenWith = BrowserChromium.instance.settings.homeButtonlink;
            creation();
        }
        public void loadPage(string url)
        {
            chromiumWebBrowser1.LoadUrl(url);

        }
        private void chromiumWebBrowser1_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
          //  this.Icon = Resources.NoSiteFavicon;
            cbTextbox1.setText(chromiumWebBrowser1.GetBrowser().MainFrame.Url);
            if (BrowserChromium.instance.favicons_loaded.TryGetValue(chromiumWebBrowser1.GetBrowser().MainFrame.Url, out var favicon))
            {
                this.Icon = favicon;
            }
            certificateInfo = new CertificateInfo(chromiumWebBrowser1.GetBrowser().MainFrame.Url);
            if(certificateInfo.cert_ != null)
            {
              //  Cef.GetGlobalCookieManager().
              //  MessageBox.Show(certificateInfo.cert_.GetExpirationDateString(),"Certificate: "+ chromiumWebBrowser1.GetBrowser().MainFrame.Url);
            }
        }
        async Task<Icon> GetIcon(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                using (var stream = await httpClient.GetStreamAsync(url))
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin); // See https://stackoverflow.com/a/72205381/640195
                    return new Icon(ms);

                }

            }
            catch (Exception e)
            {
                return Resources.NoSiteFavicon;
            }
           
            
        }
        public void OnFaviconUrlChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IList<string> urls)
        {
            if(urls.Count > 0)
            {
                foreach(string s in urls)
                {
                    if (s.Contains(".ico"))
                    {
                        if(BrowserChromium.instance.favicons_loaded.TryGetValue(browser.MainFrame.Url, out var favicon))
                        {
                            this.Icon = favicon;
                        }
                        else
                        {
                            Icon icon = GetIcon(s).Result;
                            this.Icon = icon;
                            BrowserChromium.instance.favicons_loaded.Add(browser.MainFrame.Url, icon);
                        }
                       
                    }
                    else
                    {
                      
                    }
                    BrowserChromium.instance.consoleString(s);
                }
                // this.Icon = GetIcon(urls[0]).Result;
            }
            else
            {
                this.Icon = Resources.NoSiteFavicon;
            }
           
        }
        private void cbTextbox1_Load(object sender, EventArgs e)
        {

        }
       

        private void chromiumWebBrowser1_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            Text = e.Title;
           
        }

        private void back_Click(object sender, EventArgs e)
        {
           if(chromiumWebBrowser1.CanGoBack) chromiumWebBrowser1.Back();
        }

        private void forward_Click(object sender, EventArgs e)
        {
            if (chromiumWebBrowser1.CanGoForward) chromiumWebBrowser1.Forward();
        }
        

        public bool getItemExistInPage(string id)
        {
            var script = @"if(document.getElementById('"+id+"') != null){return true;}else{ return false;}";
            var response = chromiumWebBrowser1.EvaluateScriptAsync(script);
            return response.Result.Success && (response.Result != null);
        }
        private void DownloadUpdatePage_Tick(object sender, EventArgs e)
        {
            if (chromiumWebBrowser1.IsBrowserInitialized)
            {
                if (chromiumWebBrowser1.GetMainFrame() != null)
                    if (chromiumWebBrowser1.GetMainFrame().Url == "kitsune://downloads/")
                    {
                        for (int i = 0; i < BrowserChromium.instance.downloadManager.items.Count; i++)
                        {

                            DownloadObject obj = BrowserChromium.instance.downloadManager.items[i];
                            string pauseResumeSvg = BrowserChromium.instance.downloadManager.Button_Pause;
                            if (obj.status == DownloadStatus.Paused)
                            {
                                pauseResumeSvg = BrowserChromium.instance.downloadManager.Button_Resume;
                            }
                            if (obj.status == DownloadStatus.Finished || obj.status == DownloadStatus.Canceled)
                            {
                                var script2 = "if(document.getElementById('progress-bar-ITEM-" + obj.itemName + "') != null){document.getElementById('progression-ITEM-" + obj.itemName + "').className = 'download-progression hide'; }";
                                var response2 = chromiumWebBrowser1.EvaluateScriptAsync(script2);
                            }
                            int perc = obj.PercentComplete;
                            string progressString = perc + "% - " + DownloadManager.SizeSuffix(obj.downloadedBytes) + " / " + DownloadManager.SizeSuffix(obj.totalBytes) + ", " + DownloadManager.SizeSuffix(obj.downloadSpeed) + "/s";
                            var script = "if(document.getElementById('progress-bar-ITEM-" + obj.itemName + "') != null){document.getElementById('STATUS-ITEM-" + obj.itemName + "').innerHTML = '" + progressString + "'; document.getElementById('progress-bar-ITEM-" + obj.itemName + "').style.width = '" + obj.PercentComplete + "%'; }";
                            var response = chromiumWebBrowser1.EvaluateScriptAsync(script);
                            script = "if(document.getElementById('progress-bar-ITEM-" + obj.itemName + "') != null){document.getElementById('pauseresumebutton-" + obj.itemName + "').innerHTML = '" + pauseResumeSvg + "'; }";
                            response = chromiumWebBrowser1.EvaluateScriptAsync(script);
                            if (obj.status == DownloadStatus.Finished)
                            {
                                var script2 = "if(document.getElementById('progress-bar-ITEM-" + obj.itemName + "') != null){document.getElementById('progression-ITEM-" + obj.itemName + "').className = 'download-progression hide'; }";
                                var response2 = chromiumWebBrowser1.EvaluateScriptAsync(script2);
                            }
                        }
                    }
            }
           
            int totCompleted = 0;
            int totalBytes = 0;
            int actualBytes = 0;
            for (int i = 0; i < BrowserChromium.instance.downloadManager.items.Count; i++)
            {

                DownloadObject obj = BrowserChromium.instance.downloadManager.items[i];
              
                if(obj.status == DownloadStatus.Finished || obj.status == DownloadStatus.Canceled)
                {
                    totCompleted++;
                }
                else
                {
                    totalBytes += (int)obj.totalBytes;
                    actualBytes += (int)obj.downloadedBytes;
                }
            }
            float valPercent = (float)actualBytes / (float)totalBytes;
            int percent =(int)( valPercent * 100f);
            if(percent < 0)
            {
                percent = 50;
            }
            if(totCompleted== BrowserChromium.instance.downloadManager.items.Count)
            {
                progressBarKitsune1.Visible = false;
            }
            else
            {
               
                progressBarKitsune1.Visible = true;
                progressBarKitsune1.Value = percent;
            }
            if (oldScreenMode != newScreenMode)
            {
                oldScreenMode = newScreenMode;
                fullScreen(newScreenMode);
            }

            //Manage favorites here
            if (BrowserChromium.instance.settings.favorites)
            {
                panel1.Height = 72;
                parrotToolStrip1.Visible = true;
               
            }
            else
            {
                panel1.Height = 46;
                parrotToolStrip1.Visible = false;
            }
            if (newScreenMode == false)
            {
                if (BrowserChromium.instance.settings.favorites)
                {
                    if (chromiumWebBrowser1.IsBrowserInitialized) this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 72);
                    if (chromiumWebBrowser1.IsBrowserInitialized) this.chromiumWebBrowser1.Size = new System.Drawing.Size(this.Width, this.Height - 72);
                }
                else
                {
                    if (chromiumWebBrowser1.IsBrowserInitialized) this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 46);
                    if (chromiumWebBrowser1.IsBrowserInitialized) this.chromiumWebBrowser1.Size = new System.Drawing.Size(this.Width, this.Height - 46);
                }
            }
            if (chromiumWebBrowser1.IsBrowserInitialized)
            if (getFavoriteExist(this.chromiumWebBrowser1.GetMainFrame().Url))
            {
                favoriteButton.Image = Resources.favorites_exist;
            }
            else
            {
                favoriteButton.Image = Resources.favorites;
            }
            if (chromiumWebBrowser1.IsBrowserInitialized)
            {
               /* if (chromiumWebBrowser1.CanGoForward)
                {
                    forward.Enabled = true;
                }
                else
                {
                    forward.Enabled = false;
                }
                if (chromiumWebBrowser1.CanGoBack)
                {
                    back.Enabled = true;
                }
                else
                {
                    back.Enabled = false;
                }*/
            }
        }

        private void reload_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Reload();
        }

        private void settings_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.LoadUrl("kitsune://settings/");
        }

        private void downloadslist_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.LoadUrl("kitsune://downloads/");
        }

        private void chromiumWebBrowser1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ExecuteJavascript(string script)
        {
           var result = chromiumWebBrowser1.EvaluateScriptAsync(script);
        }
        public bool oldScreenMode = false;
        public bool newScreenMode = false;
        public void fullScreen(bool Val)
        {
            if (Val)
            {
               
                System.Windows.Forms.Screen src = System.Windows.Forms.Screen.FromControl(BrowserChromium.instance);
                handler.fullScreenForm = new Form();
               
                handler.fullScreenForm.FormBorderStyle = FormBorderStyle.None;
           //     handler.fullScreenForm.WindowState = FormWindowState.Maximized;
                this.Controls.Remove(chromiumWebBrowser1);
                handler.fullScreenForm.Controls.Add(chromiumWebBrowser1);
                handler.fullScreenForm.Show();
                handler.fullScreenForm.Bounds = src.Bounds;
                BrowserChromium.instance.Hide();
                handler.fullScreenForm.Icon = Resources.kitsune;
                handler.fullScreenForm.Text = "Kitsune Browser";
                this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 0);


                this.chromiumWebBrowser1.Size = new System.Drawing.Size(handler.fullScreenForm.Width, handler.fullScreenForm.Height);
            }
            else
            {
                handler.fullScreenForm.Controls.Remove(chromiumWebBrowser1);
                this.Controls.Add(chromiumWebBrowser1);
                BrowserChromium.instance.Show();
                handler.fullScreenForm.Close();
                handler.fullScreenForm.Dispose();
                handler.fullScreenForm = null;
                if (BrowserChromium.instance.settings.favorites)
                {
                    this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 72);
                    this.chromiumWebBrowser1.Size = new System.Drawing.Size(this.Width, this.Height - 72);
                }
                else
                {
                    this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 46);
                    this.chromiumWebBrowser1.Size = new System.Drawing.Size(this.Width, this.Height - 46);
                }
               

             
               
            }
           
        }
        string favXml;
        public bool getFavoriteExist(String url)
        {
            XmlDocument myXml = new XmlDocument();

            if (File.Exists(favXml))
            {
                myXml.Load(favXml);
                foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
                {
                    if(el.GetAttribute("url") == url)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void addFavorit(String url, string name, Icon icon)
        {
            new System.Threading.Thread(() =>
            {
                if (getFavoriteExist(url))
                {
                    return;
                }
                XmlDocument myXml = new XmlDocument();
            XmlElement el = myXml.CreateElement("favorit");
            el.SetAttribute("url", url);
            el.InnerText = name;
            if (!File.Exists(favXml))
            {
                XmlElement root = myXml.CreateElement("favorites");
                myXml.AppendChild(root);
                root.AppendChild(el);
            }
            else
            {
                myXml.Load(favXml);
                myXml.DocumentElement.AppendChild(el);
            }
            if (parrotToolStrip1.Visible == true)
            {
                   
                ToolStripButton b =
                    new ToolStripButton(el.InnerText, favicon(el.GetAttribute("url")),
                    null, el.GetAttribute("url"));
                b.ToolTipText = el.GetAttribute("url");
                //the MouseUp event is used 
                //for showing the context menu of this button 
                b.MouseDown += new MouseEventHandler(b_MouseUp);
                parrotToolStrip1.Items.Add(b);
            }
            myXml.Save(favXml);
            }).Start();
        }
        public static Image favicon(String u)
        {
            Uri url = new Uri(u);
            String iconurl = "http://" + url.Host + "/favicon.ico";

            WebRequest request = WebRequest.Create(iconurl);
            try
            {
                WebResponse response = request.GetResponse();

                Stream s = response.GetResponseStream();
                return Image.FromStream(s);
            }
            catch (Exception ex)
            {
                //return a default icon in case 
                //the web site doesn`t have a favicon
                return BrowserChromium.instance.Icon.ToBitmap();
            }
        }
        public void showFavorites()
        {
            new System.Threading.Thread(() =>
            {
             
                parrotToolStrip1.Items.Clear();
                //open the xml file
                XmlDocument myXml = new XmlDocument();

                if (File.Exists(favXml))
                {
                    myXml.Load(favXml);
                    DateTime now = DateTime.Now;

                    //....


                    // historyTreeView.ShowRootLines = true;
                    foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
                    {
                        Uri site = new Uri(el.GetAttribute("url"));


                        //create a new tree node
                        ToolStripButton b =
                      new ToolStripButton(el.InnerText, favicon(el.GetAttribute("url")),
                      null, el.GetAttribute("url"));
                        b.ToolTipText = el.GetAttribute("url");
                        //add a context menu to this node
                        b.MouseDown += new MouseEventHandler(b_MouseUp);
                        //add this node to the treeview control    
                        parrotToolStrip1.Items.Add(b);
                    }
                    //....
                }
            }).Start();
           
        }
        ToolStripButton selectedFavorite = null;
        private void b_MouseUp(object sender, MouseEventArgs e)
        {
            ToolStripButton b = (ToolStripButton)sender;
           string address = b.ToolTipText;
            string name = b.Text;

            if (e.Button == MouseButtons.Left)
            {
                this.loadPage(address);
            }else if(e.Button == MouseButtons.Right)
            {
                selectedFavorite = b;
                
                this.favoritesContextMenu_.MenuItems[0].Text = BrowserChromium.instance.settings.getTranslated("contextmenu_openlinknewtab");
                this.favoritesContextMenu_.MenuItems[1].Text = BrowserChromium.instance.settings.getTranslated("contextmenu_delete");
                this.favoritesContextMenu_.Show(panel1,new Point(Cursor.Position.X - BrowserChromium.instance.Location.X, e.Y));
            }
                
        }
        internal void changeFullScreen(bool fullscreen)
        {
            newScreenMode = fullscreen;
            
        }


        private void favoriteButton_Click(object sender, EventArgs e)
        {
            addFavorit(this.chromiumWebBrowser1.GetMainFrame().Url, this.Text, this.Icon);
        }


        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (selectedFavorite != null)
            {
                string address = selectedFavorite.ToolTipText;
                string name = selectedFavorite.Text;
                BrowserChromium.instance.openLinkNewTab(address);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (selectedFavorite != null)
            {
                string address = selectedFavorite.ToolTipText;
                string name = selectedFavorite.Text;
                parrotToolStrip1.Items.RemoveByKey(address);
                XmlDocument myXml = new XmlDocument();
                myXml.Load(favXml);
                XmlElement root = myXml.DocumentElement;
                foreach (XmlElement x in root.ChildNodes)
                {
                    if (x.GetAttribute("url").Equals(address))
                    {
                        root.RemoveChild(x);
                        break;
                    }
                }

                myXml.Save(favXml);
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.LoadUrl(BrowserChromium.instance.settings.homeButtonlink);
        }
    }

    public class CustomProtocolSchemeHandler : ResourceHandler
    {
        // Specifies where you bundled app resides.
        // Basically path to your index.html
        private string frontendFolderPath;

        public CustomProtocolSchemeHandler()
        {
            frontendFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "./bundle/");
        }

        // Process request and craft response.
        public override CefReturnValue ProcessRequestAsync(IRequest request, ICallback callback)
        {
            var uri = new Uri(request.Url);
            var fileName = uri.AbsolutePath;

            var requestedFilePath = frontendFolderPath + fileName;

            if (File.Exists(requestedFilePath))
            {
                byte[] bytes = File.ReadAllBytes(requestedFilePath);
                Stream = new MemoryStream(bytes);

                var fileExtension = Path.GetExtension(fileName);
                MimeType = GetMimeType(fileExtension);

                callback.Continue();
                return CefReturnValue.Continue;
            }

            callback.Dispose();
            return CefReturnValue.Cancel;
        }
    }
    public class CertificateInfo
    {

        public X509Certificate2 cert_;
        public string url;
        public CertificateInfo(string ur)
        {
            try
            {
                url = ur;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();
                //retrieve the ssl cert and assign it to an X509Certificate object
                X509Certificate cert = request.ServicePoint.Certificate;
                //convert the X509Certificate to an X509Certificate2 object by passing it into the constructor
                X509Certificate2 cert2 = new X509Certificate2(cert);
                cert_ = cert2;
                string cn = cert2.GetIssuerName();
                string cedate = cert2.GetExpirationDateString();
                string cpub = cert2.GetPublicKeyString();
            }
            catch (Exception e)
            {

            }

            //display the cert dialog box

        }
    }
    public class CustomProtocolSchemeHandlerFactory : ISchemeHandlerFactory
    {
        public const string SchemeName = "kitsune";
        public string getTranslated(string key)
        {
            return BrowserChromium.instance.settings.getTranslated(key);
        }
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            if (schemeName == SchemeName)
            {
                if(request.Url == SchemeName+"://homepage/")
                {
                    return ResourceHandler.FromString(Resources.home.Replace("$backgroundImage$", BrowserChromium.instance.settings.backgroundImage));
                }
                if(request.Url == SchemeName+ "://messagebox.js/")
                {
                    return ResourceHandler.FromString(Resources.messagebox1);
                }
                if (request.Url == SchemeName + "://settings/")
                {
                    HtmlPageBuilder builder = new HtmlPageBuilder();
                    builder.addTag("html");
                    builder.addTag("head");
                    builder.addTitle(getTranslated("settings_page_name"));
                    builder.addStyles(Resources.style.Replace("$backgroundImage$", BrowserChromium.instance.settings.backgroundImage));
                    builder.addStyles("<style>.download-item { height: auto; }</style>");
                    builder.closeTag("head");
                    builder.addTag("body");

                    //Topbar
                    builder.addTag("div", "topbar");
                    builder.writeContent(getTranslated("settings_page_name"));
                    builder.closeTag("div");
                    builder.addTag("div", "flex-grid");
                    //Settings content
                    //Homepage settings
                    builder.addTag("div", "download-item");
                    builder.addTag("div", "item-name"); builder.writeContent("Homepage configuration"); builder.closeTag("div");

                    builder.addInputValue(getTranslated("settings_page_homepageLink"), BrowserChromium.instance.settings.homeButtonlink, "window.browserSettings.setHomepageLink(this.value);");
                    //Appearance settings


                    builder.addTag("div", "item-name"); builder.writeContent(getTranslated("settings_page_appearance")); builder.closeTag("div");

                    builder.addSettingBoolean(getTranslated("settings_page_appearance_favoritebar"), BrowserChromium.instance.settings.favorites, "window.browserSettings.setFavoritesVisibility(this.checked);");
                    // builder.closeTag("div");
                    //Language settings

                    // builder.addTag("div", "download-item");
                    builder.addTag("div", "item-name"); builder.writeContent(getTranslated("settings_page_language")); builder.closeTag("div");

                    builder.addLanguage(getTranslated("settings_page_language_browser_lang"), BrowserChromium.instance.settings.language, "window.browserSettings.setLanguage(this.options[this.selectedIndex].value);");
                    //  builder.closeTag("div");

                    //Downloads

                    // builder.addTag("div", "download-item");
                    builder.addTag("div", "item-name"); builder.writeContent("Downloads"); builder.closeTag("div");

                    //  builder.addLanguage(getTranslated("settings_page_language_browser_lang"), BrowserChromium.instance.settings.language, "window.browserSettings.setLanguage(this.options[this.selectedIndex].value);");
                    //  builder.closeTag("div");

                    //Search Engine

                 
                    builder.addTag("div", "item-name"); builder.writeContent("Search engine"); builder.closeTag("div");

                    builder.addSearchEngine(getTranslated("settings_page_search_engine"), BrowserChromium.instance.settings.searchEngine, "window.browserSettings.setSearchEngine(this.options[this.selectedIndex].value);");

                    //Cache settings


                    builder.addTag("div", "item-name"); builder.writeContent("Cache settings"); builder.closeTag("div");

                    
                    builder.closeTag("div");

                    //End here
                    builder.closeTag("div");

                    builder.closeTag("body");
                    builder.closeTag("html");
                    
                    return ResourceHandler.FromString(builder.getContent());
                }
                if (request.Url == SchemeName + "://downloads/")
                {
                    string consoleStrings = "<html>" +
                       "<head>" +
                       "<title>" +
                       "Downloads List" +
                       "</title>" +
                       Resources.style.Replace("$backgroundImage$", BrowserChromium.instance.settings.backgroundImage) +
                      
                       "</head>" +
                       "<body>" +
                       "        <div class=\"topbar\"><svg width=\"35\" height=\"35\" viewBox=\"0 0 16 16\" fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\"> <path d=\"M0.5 9.90002C0.776142 9.90002 1 10.1239 1 10.4V12.9C1 13.4523 1.44772 13.9 2 13.9H14C14.5523 13.9 15 13.4523 15 12.9V10.4C15 10.1239 15.2239 9.90002 15.5 9.90002C15.7761 9.90002 16 10.1239 16 10.4V12.9C16 14.0046 15.1046 14.9 14 14.9H2C0.895431 14.9 0 14.0046 0 12.9V10.4C0 10.1239 0.223858 9.90002 0.5 9.90002Z\" fill=\"white\"/> <path d=\"M7.64645 11.8536C7.84171 12.0488 8.15829 12.0488 8.35355 11.8536L11.3536 8.85355C11.5488 8.65829 11.5488 8.34171 11.3536 8.14645C11.1583 7.95118 10.8417 7.95118 10.6464 8.14645L8.5 10.2929V1.5C8.5 1.22386 8.27614 1 8 1C7.72386 1 7.5 1.22386 7.5 1.5V10.2929L5.35355 8.14645C5.15829 7.95118 4.84171 7.95118 4.64645 8.14645C4.45118 8.34171 4.45118 8.65829 4.64645 8.85355L7.64645 11.8536Z\" fill=\"white\"/></svg></div>" +
                       "<div class='flex-grid'>" +
                       "<div class=\"download-item\" style='height: 35px;'> <div class=\"item-name\">This session downloads</div></div>";
                    for (int i = 0; i < BrowserChromium.instance.downloadManager.items.Count; i++)
                    {
                        DownloadObject obj = BrowserChromium.instance.downloadManager.items[i];
                        consoleStrings += DownloadManager.createDivItem(obj);
                      
                     
                    }

                    consoleStrings +=
                       "</div>" +
                       "</body>" +
                       "</html>";
                    return ResourceHandler.FromString(consoleStrings);
                }
                if (request.Url == SchemeName + "://console/")
                {
                    string consoleStrings = "<html>" +
                        "<head>" +
                        "<title>" +
                        "Console" +
                        "</title>" +
                        "</head>" +
                        "<body>" ;


                    foreach(string c in BrowserChromium.instance.mainConsole)
                    {
                        consoleStrings += "<p>" + c + "</p><br>";
                    }

                    consoleStrings +=
                        "</body>" +
                        "</html>";
                    return ResourceHandler.FromString(consoleStrings);
                }
                
            }
            return new ResourceHandler();
        }
    }
}
