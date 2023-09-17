using ReaLTaiizor.Enum.Parrot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NullLib.TickAnimation;
using Win32Interop.Enums;
using System.Reflection;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using KitsuneBrowser.Properties;
using CefSharp;
using CefSharp.WinForms;

namespace KitsuneBrowser.Controls
{
   
    public partial class CBTextbox : UserControl
    {
        public Color colorBkg;
        public Color normalColor;
        public Color hoverColor;
        public Color focusColor;
        public String text;
        bool hovered;
        bool focused;
      
        Helper.DrawingTickAnimator pnColorAnimator;
        public Color nowColor
        {
            get => colorBkg; set
            {
                colorBkg = value;
                
            }
        }
        public CBTextbox()
        {
            InitializeComponent();
            colorBkg = Color.FromArgb(normalColor.ToArgb());
            pnColorAnimator = new Helper.DrawingTickAnimator(new CubicTicker(EasingMode.EaseOut), this, nameof(nowColor));
        }
        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        public Color NormalColor
        {
            get
            {
                return normalColor;
            }
            set { normalColor = value; }
        }
        public Color HoverColor
        {
            get
            {
                return hoverColor;
            }
            set { hoverColor = value; }
        }
        public Color FocusColor
        {
            get
            {
                return focusColor;
            }
            set { focusColor = value; }
        }
        public void colorAnim(Color color)
        {
         
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
              
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen penBg = new Pen(normalColor);
            
            if (hovered)
            {

                penBg.Color = hoverColor;
                if (focused)
                {
                    penBg.Color = focusColor;
                }
            }
            else
            {
                if (focused)
                {
                    penBg.Color = focusColor;
                }
            }
                Graphics g = e.Graphics;
            Brush brush = penBg.Brush;

            g.FillPath(brush,RoundedRect(e.ClipRectangle,12));
            textBox1.BackColor = penBg.Color;
            pictureBox1.BackColor = penBg.Color;
        }

        private void CBTextbox_MouseEnter(object sender, EventArgs e)
        {
            //   hovered = true;
            //  pnColorAnimator.Animate(hoverColor, 200);
            //  this.Invalidate();
        }

        private void CBTextbox_MouseLeave(object sender, EventArgs e)
        {
            //  pnColorAnimator.Animate(normalColor, 200);
            //   hovered = false;
            // this.Invalidate();
        }

        private void CBTextbox_MouseHover(object sender, EventArgs e)
        {
           // pnColorAnimator.Animate(hoverColor, 200);
         //   hovered = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            text = textBox1.Text;
        }

        private void CBTextbox_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void CBTextbox_MouseMove(object sender, MouseEventArgs e)
        {
          
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            hovered = true;
            this.Invalidate();
        }
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            hovered = false;
            this.Invalidate();
        }
        public void setText(String Text)
        {
            textBox1.Text = Text;
            text = Text;
            
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            focused = true;
            this.Invalidate();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            focused = false;
            this.Invalidate();
        }
        
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Uri uri;
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
               
                if(System.Uri.TryCreate(textBox1.Text, UriKind.Absolute, out uri))
                {
                    TabWindow win = (TabWindow)BrowserChromium.instance.SelectedTab.Content;
                    win.loadPage(textBox1.Text);
                }
                else
                {
                    if(BrowserChromium.instance.settings.searchEngine == SearchEngine.google)
                    {
                        TabWindow win = (TabWindow)BrowserChromium.instance.SelectedTab.Content;
                        win.loadPage("https://www.google.com/search?q=" + textBox1.Text);
                    }
                    else if (BrowserChromium.instance.settings.searchEngine == SearchEngine.bing)
                    {
                        TabWindow win = (TabWindow)BrowserChromium.instance.SelectedTab.Content;
                        win.loadPage("https://bing.com/search?q=" + textBox1.Text);
                    }

                }
                
            }
        }

        private void invalidation_Tick(object sender, EventArgs e)
        {
           
                TabWindow win = (TabWindow)BrowserChromium.instance.SelectedTab.Content;
            Uri uri;
            if (win.chromiumWebBrowser1.IsBrowserInitialized)
            {

            }
            else
            {
                return;
            }
                //  if (win.chromiumWebBrowser1.IsLoading) return;
                if (System.Uri.TryCreate(textBox1.Text, UriKind.Absolute, out uri))
            {
               
                if (textBox1.Text == win.chromiumWebBrowser1.GetMainFrame().Url)
                {
                    if(win.certificateInfo != null)
                    {
                        if(win.certificateInfo.cert_ != null) {
                            this.pictureBox1.Image = Resources.https_secure;
                        }
                        else
                        {
                            if (textBox1.Text.StartsWith("kitsune://"))
                            {
                                this.pictureBox1.Image = Resources.kitsune.ToBitmap();
                            }
                            else
                            {
                                this.pictureBox1.Image = Resources.no_secure;
                            }
                           
                        }
                    }
                    else
                    {
                       
                       
                    }
                   
                }
                else
                {
                    
                    this.pictureBox1.Image = Resources.NoSiteFavicon.ToBitmap();
                }
                
            }
            else
            {
                this.pictureBox1.Image = Resources.kitsune.ToBitmap();
            }
            //  this.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TabWindow win = (TabWindow)BrowserChromium.instance.SelectedTab.Content;
            WebPageCertificate form = new WebPageCertificate(win.certificateInfo);
            form.Show();

        }

        private void CBTextbox_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
   
}
