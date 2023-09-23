using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitsuneBrowser
{
    public class HtmlPageBuilder
    {
        private string content;

        public void addTag(string tag)
        {
            content += "<"+tag+">";
        }
        public void closeTag(string tag)
        {
            content += "</" + tag + ">";
        }
        public void addTag(string tag, string classes)
        {
            content += "<" + tag + " class='"+classes+"'>";
        }
        public void addTitle(string title)
        {
            addTag("title");
            writeContent(title);
            closeTag("title");
        }
        public void addInput(string type, string classes, string name, bool val, string clickfunc)
        {
            string isChecked = "";
            if (val)
            {
                isChecked = "checked";
            }
            writeContent("<input type='"+type+"' class='"+classes+"' name='"+name+"' "+isChecked+" onclick=\""+clickfunc+"\"/>");
        }
        public void addLanguage(string title, Language language, string clickfunc)
        {
            writeContent("<div style='margin-left:15px; margin-top: 5px; margin-right: 15px; height: 40px;'>");
            writeContent("<div style='float: left;'>");

            writeContent(title);
            closeTag("div");
            writeContent("<div style='float: right;'>");
            writeContent("<select name=\"language\" id=\"language\" value='"+language.ToString()+ "' onclick=\""+clickfunc+"\">");
            writeContent("<option value=\""+language.ToString()+"\" >"+BrowserChromium.instance.settings.getTranslated("lang_name")+"</option>");
            if (language != Language.en) writeContent("<option value=\"en\">English</option>");
            if (language != Language.it) writeContent("<option value=\"it\">Italiano</option>");
            writeContent("</select>");
            closeTag("div");
            closeTag("div");
        }

        public void addSearchEngine(string title, SearchEngine language, string clickfunc)
        {
            writeContent("<div style='margin-left:15px; margin-top: 5px; margin-right: 15px; height: 40px;'>");
            writeContent("<div style='float: left;'>");

            writeContent(title);
            closeTag("div");
            writeContent("<div style='float: right;'>");
            writeContent("<select name=\"language\" id=\"language\" value='" + language.ToString() + "' onclick=\"" + clickfunc + "\">");
            writeContent("<option value=\"" + language.ToString() + "\" >" + language.ToString() + "</option>");
            if (language != SearchEngine.google) writeContent("<option value=\"google\">google</option>");
            if (language != SearchEngine.bing) writeContent("<option value=\"bing\">bing</option>");
            writeContent("</select>");
            closeTag("div");
            closeTag("div");
        }
        public void addSettingBoolean(string checkboxtitle, bool defaultValue, string clickfunc)
        {

            writeContent("<div style='margin-left:15px; margin-top: 5px; margin-right: 15px; height: 40px;'>");
            writeContent("<div style='float: left;'>");

            writeContent(checkboxtitle);
            closeTag("div");
            writeContent("<div style='float: right;'>");
            addInput("checkbox","checkbox",checkboxtitle, defaultValue, clickfunc);
            closeTag("div");
            closeTag("div");
        }
        public void addStyles(string styleContent)
        {
            writeContent(styleContent);
        }
        public void writeContent(string cont)
        {
            content += cont;
        }

        public string getContent()
        {
            return content;
        }

        internal void addInputValue(string title, string homeButtonlink, string clickfunc)
        {
            writeContent("<div style='margin-left:15px; margin-top: 5px; margin-right: 15px; height: 40px;'>");
            writeContent("<div style='float: left;'>");

            writeContent(title);
            closeTag("div");
            writeContent("<div style='float: right;'>");
            writeContent("<input name=\"inputval\" id=\"inputval\" value='" + homeButtonlink + "' onkeydown=\"" + clickfunc + "\"  />");
   
            closeTag("div");
            closeTag("div");
        }
    }
}
