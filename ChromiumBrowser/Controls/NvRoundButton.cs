using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitsuneBrowser.Controls
{
    public partial class NvRoundButton : UserControl
    {

        public Color normalColor;
        public Color hoverColor;
        public Color focusColor;
        public Image image;
        bool hovered;
        bool focused;
        public NvRoundButton()
        {
            InitializeComponent();
            pictureBox1.Image = image;
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
        public Image ButtonImage
        {
            get
            {
                return image;
            }
            set { image = value; }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
           // base.OnPaint(e);
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

            }
            Graphics g = e.Graphics;
            Brush brush = penBg.Brush;

            g.FillPath(brush, RoundedRect(e.ClipRectangle, 12));
          
        }
    }
}
