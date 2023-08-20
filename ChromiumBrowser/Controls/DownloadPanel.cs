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
    public partial class DownloadPanel : UserControl
    {
      
        public DownloadPanel()
        {
            InitializeComponent();

           SetStyle(ControlStyles.Opaque, false);
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
       
        protected override void OnPaint(PaintEventArgs e)
        {
            //   this.BackColor = Color.Transparent;


            Pen penBg = new Pen(Color.FromArgb(37, 40, 55));

            Graphics g = e.Graphics;
            Brush brush = penBg.Brush;

            g.FillPath(brush, RoundedRect(this.ClientRectangle, 12));
            base.OnPaint(e);
        }
       /* protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT; // WS_EX_TRANSPARENT
                return cp;
            }
        }
       */
        protected override void OnPaintBackground(PaintEventArgs e)
        {


            Pen penBg = new Pen(Color.FromArgb(37, 40, 55));

            Graphics g = e.Graphics;
            Brush brush = penBg.Brush;

            g.FillPath(brush, RoundedRect(this.ClientRectangle, 12));
            g.Clear(Color.Transparent);
           // base.OnPaintBackground(e);
        }
        private void repaint_Tick(object sender, EventArgs e)
        {

          

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
