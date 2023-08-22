using KitsuneBrowser.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32Interop.Enums;


namespace KitsuneBrowser
{
    public partial class WebPageCertificate : Form
    {
        public WebPageCertificate(CertificateInfo certificate)
        {
            InitializeComponent();

            if (certificate != null)
            {
                this.Text = "Certificate of: "+certificate.url;
                if (certificate.cert_ != null)
                {
                    this.label1.Text = "The connection is secure";
                    this.pictureBox1.Image = Resources.https_secure;
                    this.label2.Text = "Expiration date: "+certificate.cert_.GetExpirationDateString();
                }
                else
                {
                    if (certificate.url.StartsWith("kitsune://"))
                    {
                        this.label2.Text = "This is an incorporated Kitsune Browser page";
                        this.label1.Text = "Kitsune local page";
                       
                        this.pictureBox1.Image = Resources.kitsune.ToBitmap();
                    }
                    else
                    {
                        this.label2.Text = "Null";
                        this.label1.Text = "The connection is not secure";
                        this.pictureBox1.Image = Resources.no_secure;
                    }
                 
                }
            }
            else
            {
               
            }
               

        }

      
    }
}
