using System;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using EasyTabs;

namespace KitsuneBrowser
{
    internal static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BrowserChromium testApp = new BrowserChromium();
           
            testApp.Tabs.Add(
                new TitleBarTab(testApp)
                {
                    Content = new TabWindow
                    {
                        Text = "New Tab"
                    }
                });
            testApp.SelectedTabIndex = 0;

            TitleBarTabsApplicationContext applicationContext = new TitleBarTabsApplicationContext();
            applicationContext.Start(testApp);

            Application.Run(applicationContext);
        }
    }
}
