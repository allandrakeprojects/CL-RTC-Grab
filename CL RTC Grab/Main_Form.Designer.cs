﻿namespace CL_RTC_Grab
{
    partial class Main_Form
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
        /// Required method for Designer support - do not modiCL
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.panel_header = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.pictureBox_minimize = new System.Windows.Forms.PictureBox();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.label_brand = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox_loader = new System.Windows.Forms.PictureBox();
            this.timer_detect = new System.Windows.Forms.Timer(this.components);
            this.label_player_last_registered = new System.Windows.Forms.Label();
            this.timer_close_message_box = new System.Windows.Forms.Timer(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel_landing = new System.Windows.Forms.Panel();
            this.pictureBox_landing = new System.Windows.Forms.PictureBox();
            this.timer_landing = new System.Windows.Forms.Timer(this.components);
            this.timer_fill = new System.Windows.Forms.Timer(this.components);
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.timer_flush_memory = new System.Windows.Forms.Timer(this.components);
            this.panel_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_loader)).BeginInit();
            this.panel_landing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_landing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_header
            // 
            this.panel_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(90)))), ((int)(((byte)(101)))));
            this.panel_header.Controls.Add(this.panel1);
            this.panel_header.Controls.Add(this.label_title);
            this.panel_header.Controls.Add(this.pictureBox_minimize);
            this.panel_header.Controls.Add(this.pictureBox_close);
            this.panel_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_header.Location = new System.Drawing.Point(0, 0);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(466, 45);
            this.panel_header.TabIndex = 1;
            this.panel_header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_header_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(96)))), ((int)(((byte)(173)))));
            this.panel1.Location = new System.Drawing.Point(0, -5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 10);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = true;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label_title
            // 
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label_title.Location = new System.Drawing.Point(1, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(145, 45);
            this.label_title.TabIndex = 2;
            this.label_title.Text = "RTC Grab";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_title_MouseDown);
            // 
            // pictureBox_minimize
            // 
            this.pictureBox_minimize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_minimize.Image = global::CL_RTC_Grab.Properties.Resources.minus;
            this.pictureBox_minimize.Location = new System.Drawing.Point(378, 10);
            this.pictureBox_minimize.Name = "pictureBox_minimize";
            this.pictureBox_minimize.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_minimize.TabIndex = 1;
            this.pictureBox_minimize.TabStop = false;
            this.pictureBox_minimize.Click += new System.EventHandler(this.pictureBox_minimize_Click);
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_close.Image = global::CL_RTC_Grab.Properties.Resources.close;
            this.pictureBox_close.Location = new System.Drawing.Point(416, 10);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_close.TabIndex = 0;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(10, 55);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(445, 402);
            this.webBrowser.TabIndex = 2;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompletedAsync);
            // 
            // label_brand
            // 
            this.label_brand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_brand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(96)))), ((int)(((byte)(173)))));
            this.label_brand.Location = new System.Drawing.Point(0, 176);
            this.label_brand.Name = "label_brand";
            this.label_brand.Size = new System.Drawing.Size(468, 23);
            this.label_brand.TabIndex = 4;
            this.label_brand.Text = "Chang Lee";
            this.label_brand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_brand.Visible = false;
            this.label_brand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_brand_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(96)))), ((int)(((byte)(173)))));
            this.panel2.Location = new System.Drawing.Point(309, 462);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 10);
            this.panel2.TabIndex = 5;
            this.panel2.TabStop = true;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // pictureBox_loader
            // 
            this.pictureBox_loader.Image = global::CL_RTC_Grab.Properties.Resources.rtc_loader;
            this.pictureBox_loader.Location = new System.Drawing.Point(85, 157);
            this.pictureBox_loader.Name = "pictureBox_loader";
            this.pictureBox_loader.Size = new System.Drawing.Size(300, 224);
            this.pictureBox_loader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_loader.TabIndex = 3;
            this.pictureBox_loader.TabStop = false;
            this.pictureBox_loader.Visible = false;
            this.pictureBox_loader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_loader_MouseDown);
            // 
            // timer_detect
            // 
            this.timer_detect.Enabled = true;
            this.timer_detect.Interval = 1200000;
            this.timer_detect.Tick += new System.EventHandler(this.timer_detect_Tick);
            // 
            // label_player_last_registered
            // 
            this.label_player_last_registered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_player_last_registered.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_player_last_registered.Location = new System.Drawing.Point(0, 47);
            this.label_player_last_registered.Name = "label_player_last_registered";
            this.label_player_last_registered.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label_player_last_registered.Size = new System.Drawing.Size(466, 23);
            this.label_player_last_registered.TabIndex = 8;
            this.label_player_last_registered.Text = "-";
            this.label_player_last_registered.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_player_last_registered.Visible = false;
            this.label_player_last_registered.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_player_last_registered_MouseDown);
            // 
            // timer_close_message_box
            // 
            this.timer_close_message_box.Enabled = true;
            this.timer_close_message_box.Tick += new System.EventHandler(this.timer_close_message_box_Tick);
            // 
            // timer
            // 
            this.timer.Interval = 30000;
            this.timer.Tick += new System.EventHandler(this.timer_TickAsync);
            // 
            // panel_landing
            // 
            this.panel_landing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(90)))), ((int)(((byte)(101)))));
            this.panel_landing.Controls.Add(this.pictureBox_landing);
            this.panel_landing.Location = new System.Drawing.Point(0, 10);
            this.panel_landing.Name = "panel_landing";
            this.panel_landing.Size = new System.Drawing.Size(468, 457);
            this.panel_landing.TabIndex = 11;
            this.panel_landing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_landing_MouseDown);
            // 
            // pictureBox_landing
            // 
            this.pictureBox_landing.Image = global::CL_RTC_Grab.Properties.Resources.rtc_cl;
            this.pictureBox_landing.Location = new System.Drawing.Point(183, 169);
            this.pictureBox_landing.Name = "pictureBox_landing";
            this.pictureBox_landing.Size = new System.Drawing.Size(111, 113);
            this.pictureBox_landing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_landing.TabIndex = 0;
            this.pictureBox_landing.TabStop = false;
            this.pictureBox_landing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_landing_MouseDown);
            // 
            // timer_landing
            // 
            this.timer_landing.Interval = 2000;
            this.timer_landing.Tick += new System.EventHandler(this.timer_landing_Tick);
            // 
            // timer_fill
            // 
            this.timer_fill.Interval = 2000;
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // timer_flush_memory
            // 
            this.timer_flush_memory.Enabled = true;
            this.timer_flush_memory.Interval = 60000;
            this.timer_flush_memory.Tick += new System.EventHandler(this.timer_flush_memory_Tick);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(25)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(466, 468);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_landing);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.label_player_last_registered);
            this.Controls.Add(this.label_brand);
            this.Controls.Add(this.pictureBox_loader);
            this.Controls.Add(this.panel_header);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(466, 468);
            this.MinimumSize = new System.Drawing.Size(466, 468);
            this.Name = "Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "timer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Form_FormClosing);
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.panel_header.ResumeLayout(false);
            this.panel_header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_loader)).EndInit();
            this.panel_landing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_landing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.PictureBox pictureBox_minimize;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.PictureBox pictureBox_loader;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Label label_brand;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer_detect;
        private System.Windows.Forms.Label label_player_last_registered;
        private System.Windows.Forms.Timer timer_close_message_box;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panel_landing;
        private System.Windows.Forms.PictureBox pictureBox_landing;
        private System.Windows.Forms.Timer timer_landing;
        private System.Windows.Forms.Timer timer_fill;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.Timer timer_flush_memory;
    }
}
