using KitsuneBrowser.Properties;
using libc.translation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;

namespace KitsuneBrowser
{

    public enum Language
    {
        en,
        it
    }
    public enum SearchEngine
    {
        google,
        bing
    }
    public class Settings
    {
        public ILocalizer localizer;
        //Language
        public Language language = Language.en;
        //SearchEngine
        public SearchEngine searchEngine = SearchEngine.google;
        //Homepage

        public string backgroundImage = "https://cdn.donmai.us/original/83/79/__yae_miko_genshin_impact_drawn_by_shade_of_a_cat__8379c9f5b122f17459aab2db244139a5.png";

        //favorites
        public bool favorites = true; 

        XmlDocument settingsXml;
        public void SetSearchEngine(string engine)
        {
            Enum.TryParse(engine, out searchEngine);
            saveString("searchEngine", searchEngine.ToString());
        }
        public void SetLanguage(string lang)
        {
            Enum.TryParse(lang, out language);
            saveString("language", language.ToString());
          
        }

        public void SetFavoritesVisibility(bool val)
        {
            favorites = val;
            saveString("favoritesBar", val.ToString());
            if (BrowserChromium.instance.SelectedTab.Content != null)
            {
                TabWindow win = (TabWindow)BrowserChromium.instance.SelectedTab.Content;
                if(win.newScreenMode == false)
                if (BrowserChromium.instance.settings.favorites)
                {
                     win.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 72);
                     win.chromiumWebBrowser1.Size = new System.Drawing.Size(win.Width, win.Height - 72);
                }
                else
                {
                     win.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 46);
                     win.chromiumWebBrowser1.Size = new System.Drawing.Size(win.Width, win.Height - 46);
                }
            }
        }
        string settingsFile = "";
        public Settings()
        {
            settingsXml = new XmlDocument();
            CultureInfo.CurrentCulture = new CultureInfo("en");
            ILocalizationSource source = new JsonLocalizationSource(new FileInfo(Assembly.GetAssembly(typeof(Settings)).Location.Replace("KitsuneBrowser.exe","") + @"i18n.json"), PropertyCaseSensitivity.CaseSensitive);
            localizer = new Localizer(source, "en");
       
            settingsFile = Assembly.GetAssembly(typeof(Settings)).Location.Replace("KitsuneBrowser.exe", "") + @"\BrowserSettings.xml";
           
            if (File.Exists(settingsFile))
            {
                settingsXml.Load(settingsFile);
            }
            else
            {

                XmlDocumentType doctype= settingsXml.CreateDocumentType("Settings", null, null, "<!ELEMENT option ANY>  <!ATTLIST option id ID #REQUIRED>");
                settingsXml.AppendChild(doctype);
                XmlElement root = settingsXml.CreateElement("Settings");
                settingsXml.AppendChild(root);
            }
            loadSettings();
            settingsXml.Save(settingsFile);
        }
        public void loadSettings()
        {
            Enum.TryParse(loadString("searchEngine", searchEngine.ToString()), out searchEngine);
            Enum.TryParse(loadString("language", language.ToString()), out language);
            bool.TryParse(loadString("favoritesBar", favorites.ToString()), out favorites);
        }
        public void saveString(string name, string val)
        {
            if (settingsXml.GetElementById(name) != null)
            {
                settingsXml.GetElementById(name).InnerText = val;
                settingsXml.Save(settingsFile);
            }
        }
        public string loadString(string name, string val)
        {
            if (settingsXml.GetElementById(name) != null)
            {
                val = settingsXml.GetElementById(name).InnerText;
            }
            else
            {
                XmlElement el = settingsXml.CreateElement("option");
                el.SetAttribute("id", name);
                el.InnerText = val;
                settingsXml.DocumentElement.AppendChild(el);
                
            }
            return val;
            
        }
        public string getTranslated(string key)
        {
            return localizer.Get((string)language.ToString(), key);
          //  return localizer.Get((string)"it", key);
        }
    }
}
