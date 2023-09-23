namespace KitsuneBrowser
{
    partial class TabWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabWindow));
          

            this.panel1 = new System.Windows.Forms.Panel();
            this.favoriteButton = new ReaLTaiizor.Controls.Button();
            this.parrotToolStrip1 = new ReaLTaiizor.Controls.ParrotToolStrip();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.downloadslist = new ReaLTaiizor.Controls.Button();
            this.settings = new ReaLTaiizor.Controls.Button();
            this.reload = new ReaLTaiizor.Controls.Button();
            this.forward = new ReaLTaiizor.Controls.Button();
            this.back = new ReaLTaiizor.Controls.Button();
            this.cbTextbox1 = new KitsuneBrowser.Controls.CBTextbox();
            this.DownloadUpdatePage = new System.Windows.Forms.Timer(this.components);
            this.favoritesContextMenu_ = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
          
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.panel1.Controls.Add(this.favoriteButton);
            this.panel1.Controls.Add(this.parrotToolStrip1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.downloadslist);
            this.panel1.Controls.Add(this.settings);
            this.panel1.Controls.Add(this.reload);
            this.panel1.Controls.Add(this.forward);
            this.panel1.Controls.Add(this.back);
            this.panel1.Controls.Add(this.cbTextbox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 72);
            this.panel1.TabIndex = 1;
            // 
            // favoriteButton
            // 
            this.favoriteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.favoriteButton.BackColor = System.Drawing.Color.Transparent;
            this.favoriteButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.favoriteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.favoriteButton.EnteredBorderColor = System.Drawing.Color.Transparent;
            this.favoriteButton.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.favoriteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.favoriteButton.Image = global::KitsuneBrowser.Properties.Resources.favorites;
            this.favoriteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.favoriteButton.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.favoriteButton.Location = new System.Drawing.Point(1166, 8);
            this.favoriteButton.Name = "favoriteButton";
            this.favoriteButton.PressedBorderColor = System.Drawing.Color.Transparent;
            this.favoriteButton.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.favoriteButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.favoriteButton.Size = new System.Drawing.Size(30, 30);
            this.favoriteButton.TabIndex = 10;
            this.favoriteButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.favoriteButton.Click += new System.EventHandler(this.favoriteButton_Click);
            // 
            // parrotToolStrip1
            // 
            this.parrotToolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parrotToolStrip1.AutoSize = false;
            this.parrotToolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.parrotToolStrip1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.parrotToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.parrotToolStrip1.ForeColor = System.Drawing.Color.Black;
            this.parrotToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.parrotToolStrip1.Location = new System.Drawing.Point(7, 47);
            this.parrotToolStrip1.Name = "parrotToolStrip1";
            this.parrotToolStrip1.Size = new System.Drawing.Size(1264, 25);
            this.parrotToolStrip1.TabIndex = 9;
            this.parrotToolStrip1.Text = "parrotToolStrip1";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(1202, 36);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(30, 5);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Value = 35;
            this.progressBar1.Visible = false;
            // 
            // downloadslist
            // 
            this.downloadslist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadslist.BackColor = System.Drawing.Color.Transparent;
            this.downloadslist.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.downloadslist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadslist.EnteredBorderColor = System.Drawing.Color.Transparent;
            this.downloadslist.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.downloadslist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.downloadslist.Image = global::KitsuneBrowser.Properties.Resources.download;
            this.downloadslist.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.downloadslist.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.downloadslist.Location = new System.Drawing.Point(1202, 8);
            this.downloadslist.Name = "downloadslist";
            this.downloadslist.PressedBorderColor = System.Drawing.Color.Transparent;
            this.downloadslist.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.downloadslist.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.downloadslist.Size = new System.Drawing.Size(30, 30);
            this.downloadslist.TabIndex = 6;
            this.downloadslist.TextAlignment = System.Drawing.StringAlignment.Center;
            this.downloadslist.Click += new System.EventHandler(this.downloadslist_Click);
            // 
            // settings
            // 
            this.settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settings.BackColor = System.Drawing.Color.Transparent;
            this.settings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settings.EnteredBorderColor = System.Drawing.Color.Transparent;
            this.settings.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.settings.Image = global::KitsuneBrowser.Properties.Resources.settings;
            this.settings.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.settings.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.settings.Location = new System.Drawing.Point(1238, 8);
            this.settings.Name = "settings";
            this.settings.PressedBorderColor = System.Drawing.Color.Transparent;
            this.settings.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.settings.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.settings.Size = new System.Drawing.Size(30, 30);
            this.settings.TabIndex = 5;
            this.settings.TextAlignment = System.Drawing.StringAlignment.Center;
            this.settings.Click += new System.EventHandler(this.settings_Click);
            // 
            // reload
            // 
            this.reload.BackColor = System.Drawing.Color.Transparent;
            this.reload.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.reload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.reload.EnteredBorderColor = System.Drawing.Color.Transparent;
            this.reload.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.reload.Image = global::KitsuneBrowser.Properties.Resources.reload;
            this.reload.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.reload.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.reload.Location = new System.Drawing.Point(79, 8);
            this.reload.Name = "reload";
            this.reload.PressedBorderColor = System.Drawing.Color.Transparent;
            this.reload.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.reload.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.reload.Size = new System.Drawing.Size(30, 30);
            this.reload.TabIndex = 4;
            this.reload.TextAlignment = System.Drawing.StringAlignment.Center;
            this.reload.Click += new System.EventHandler(this.reload_Click);
            // 
            // forward
            // 
            this.forward.BackColor = System.Drawing.Color.Transparent;
            this.forward.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.forward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.forward.EnteredBorderColor = System.Drawing.Color.Transparent;
            this.forward.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.forward.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.forward.Image = global::KitsuneBrowser.Properties.Resources.forward;
            this.forward.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.forward.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.forward.Location = new System.Drawing.Point(43, 8);
            this.forward.Name = "forward";
            this.forward.PressedBorderColor = System.Drawing.Color.Transparent;
            this.forward.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.forward.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.forward.Size = new System.Drawing.Size(30, 30);
            this.forward.TabIndex = 3;
            this.forward.TextAlignment = System.Drawing.StringAlignment.Center;
            this.forward.Click += new System.EventHandler(this.forward_Click);
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.Color.Transparent;
            this.back.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.back.EnteredBorderColor = System.Drawing.Color.Transparent;
            this.back.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.back.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.back.Image = global::KitsuneBrowser.Properties.Resources.back;
            this.back.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.back.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
            this.back.Location = new System.Drawing.Point(7, 8);
            this.back.Name = "back";
            this.back.PressedBorderColor = System.Drawing.Color.Transparent;
            this.back.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.back.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.back.Size = new System.Drawing.Size(30, 30);
            this.back.TabIndex = 2;
            this.back.TextAlignment = System.Drawing.StringAlignment.Center;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // cbTextbox1
            // 
            this.cbTextbox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTextbox1.BackColor = System.Drawing.Color.Transparent;
            this.cbTextbox1.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.cbTextbox1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(101)))));
            this.cbTextbox1.Location = new System.Drawing.Point(115, 5);
            this.cbTextbox1.Name = "cbTextbox1";
            this.cbTextbox1.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(59)))), ((int)(((byte)(74)))));
          
            this.cbTextbox1.Padding = new System.Windows.Forms.Padding(7);
            this.cbTextbox1.Size = new System.Drawing.Size(1040, 33);
            this.cbTextbox1.TabIndex = 0;
            this.cbTextbox1.Load += new System.EventHandler(this.cbTextbox1_Load);
            // 
            // DownloadUpdatePage
            // 
            this.DownloadUpdatePage.Enabled = true;
            this.DownloadUpdatePage.Interval = 1000;
            this.DownloadUpdatePage.Tick += new System.EventHandler(this.DownloadUpdatePage_Tick);
            // 
            // favoritesContextMenu_
            // 
            this.favoritesContextMenu_.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Open link to new tab";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Delete";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // TabWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1280, 426);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TabWindow";
            this.Text = "TabWindow";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.Button back;
        private Controls.CBTextbox cbTextbox1;
        private ReaLTaiizor.Controls.Button forward;
        private ReaLTaiizor.Controls.Button reload;
        private System.Windows.Forms.Timer DownloadUpdatePage;
        private ReaLTaiizor.Controls.Button downloadslist;
        private ReaLTaiizor.Controls.Button settings;
        private System.Windows.Forms.ProgressBar progressBar1;
        private ReaLTaiizor.Controls.ParrotToolStrip parrotToolStrip1;
        private ReaLTaiizor.Controls.Button favoriteButton;
        private System.Windows.Forms.ContextMenu favoritesContextMenu_;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}