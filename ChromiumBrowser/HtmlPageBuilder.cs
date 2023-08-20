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
        public void addSettingBoolean(string checkboxtitle, bool defaultValue, string clickfunc)
        {

            writeContent("<div style='margin-left:15px; margin-top: 5px; margin-right: 15px;'>");
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
    }
}
