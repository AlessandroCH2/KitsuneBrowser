using KitsuneBrowser.Properties;
using libc.translation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitsuneBrowser
{
    public class Settings
    {
        public ILocalizer localizer;
        //Homepage
        public bool animeEpisodesDay = true;
        public string backgroundImage = "https://cdn.donmai.us/original/83/79/__yae_miko_genshin_impact_drawn_by_shade_of_a_cat__8379c9f5b122f17459aab2db244139a5.png";

        //favorites
        public bool favorites = true; //Coming soon

        public void SetAnimeEpisodeDay(bool val)
        {
            animeEpisodesDay = val;
        }
        public void SetFavoritesVisibility(bool val)
        {
            favorites = val;
            if(BrowserChromium.instance.SelectedTab.Content != null)
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
        public Settings()
        {
            BrowserChromium.instance.consoleString(Assembly.GetAssembly(typeof(Settings)).Location + @"i18n.json");
            CultureInfo.CurrentCulture = new CultureInfo("en");
            ILocalizationSource source = new JsonLocalizationSource(new FileInfo(Assembly.GetAssembly(typeof(Settings)).Location.Replace("KitsuneBrowser.exe","") + @"i18n.json"), PropertyCaseSensitivity.CaseSensitive);
            localizer = new Localizer(source, "en");
            BrowserChromium.instance.consoleString("lang: " + (string)Properties.Settings.Default["language"]);
        }
        public string getTranslated(string key)
        {
            return localizer.Get((string)Properties.Settings.Default["language"], key);
          //  return localizer.Get((string)"it", key);
        }
    }
}
