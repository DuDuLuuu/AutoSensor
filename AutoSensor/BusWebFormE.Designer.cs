namespace AutoSensor
{
    partial class BusWebFormE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusWebFormE));
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.refreshInterval = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_beginRefresh = new System.Windows.Forms.Button();
            this.button_gotoHome = new System.Windows.Forms.Button();
            this.button_manualRefresh = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_lastTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_uploadCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_failNotice = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.rectangleShape1 = new CYControls.RectangleShape();
            this.timer10 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(0, 62);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.webBrowser.MinimumSize = new System.Drawing.Size(27, 25);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(1175, 469);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            this.webBrowser.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser_NewWindow);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.refreshInterval);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button_Stop);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_beginRefresh);
            this.panel1.Controls.Add(this.button_gotoHome);
            this.panel1.Controls.Add(this.button_manualRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1175, 55);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(825, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "秒";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(662, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "刷新间隔：";
            // 
            // refreshInterval
            // 
            this.refreshInterval.Location = new System.Drawing.Point(749, 17);
            this.refreshInterval.Name = "refreshInterval";
            this.refreshInterval.Size = new System.Drawing.Size(70, 25);
            this.refreshInterval.TabIndex = 6;
            this.refreshInterval.Text = "300";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 14);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 5;
            this.button1.Text = "查看账单";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(549, 14);
            this.button_Stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(107, 29);
            this.button_Stop.TabIndex = 4;
            this.button_Stop.Text = "停止自动刷新";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(867, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(427, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "为保障您的账户安全，请您自行完成账户登录后，点击刷新页面";
            // 
            // button_beginRefresh
            // 
            this.button_beginRefresh.Location = new System.Drawing.Point(379, 14);
            this.button_beginRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.button_beginRefresh.Name = "button_beginRefresh";
            this.button_beginRefresh.Size = new System.Drawing.Size(165, 29);
            this.button_beginRefresh.TabIndex = 2;
            this.button_beginRefresh.Text = "开始自动刷新页面";
            this.button_beginRefresh.UseVisualStyleBackColor = true;
            this.button_beginRefresh.Click += new System.EventHandler(this.button_beginRefresh_Click);
            // 
            // button_gotoHome
            // 
            this.button_gotoHome.Location = new System.Drawing.Point(31, 14);
            this.button_gotoHome.Margin = new System.Windows.Forms.Padding(4);
            this.button_gotoHome.Name = "button_gotoHome";
            this.button_gotoHome.Size = new System.Drawing.Size(100, 29);
            this.button_gotoHome.TabIndex = 1;
            this.button_gotoHome.Text = "回到首页";
            this.button_gotoHome.UseVisualStyleBackColor = true;
            this.button_gotoHome.Click += new System.EventHandler(this.button_gotoHome_Click);
            // 
            // button_manualRefresh
            // 
            this.button_manualRefresh.Location = new System.Drawing.Point(247, 14);
            this.button_manualRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.button_manualRefresh.Name = "button_manualRefresh";
            this.button_manualRefresh.Size = new System.Drawing.Size(123, 29);
            this.button_manualRefresh.TabIndex = 0;
            this.button_manualRefresh.Text = "手动刷新页面";
            this.button_manualRefresh.UseVisualStyleBackColor = true;
            this.button_manualRefresh.Click += new System.EventHandler(this.button_manualRefresh_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_lastTime,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel_uploadCnt,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel_failNotice});
            this.statusStrip1.Location = new System.Drawing.Point(0, 537);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1175, 25);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_lastTime
            // 
            this.toolStripStatusLabel_lastTime.Name = "toolStripStatusLabel_lastTime";
            this.toolStripStatusLabel_lastTime.Size = new System.Drawing.Size(103, 20);
            this.toolStripStatusLabel_lastTime.Text = "最后刷新时间:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // toolStripStatusLabel_uploadCnt
            // 
            this.toolStripStatusLabel_uploadCnt.Name = "toolStripStatusLabel_uploadCnt";
            this.toolStripStatusLabel_uploadCnt.Size = new System.Drawing.Size(174, 20);
            this.toolStripStatusLabel_uploadCnt.Text = "最后一次整页刷新条数：";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel3.Text = "|";
            // 
            // toolStripStatusLabel_failNotice
            // 
            this.toolStripStatusLabel_failNotice.Name = "toolStripStatusLabel_failNotice";
            this.toolStripStatusLabel_failNotice.Size = new System.Drawing.Size(84, 20);
            this.toolStripStatusLabel_failNotice.Text = "无上报失败";
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rectangleShape1.BackColor = System.Drawing.Color.Transparent;
            this.rectangleShape1.BorderThickness = 1F;
            this.rectangleShape1.Location = new System.Drawing.Point(0, 72);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Opacity = 1F;
            this.rectangleShape1.RotateAngle = 0F;
            this.rectangleShape1.Size = new System.Drawing.Size(1120, 459);
            this.rectangleShape1.TabIndex = 3;
            // 
            // timer10
            // 
            this.timer10.Enabled = true;
            this.timer10.Interval = 10000;
            this.timer10.Tick += new System.EventHandler(this.timer10_Tick);
            // 
            // BusWebFormE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 562);
            this.Controls.Add(this.rectangleShape1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.webBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BusWebFormE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "感知收款";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BusWebFormE_FormClosing);
            this.Load += new System.EventHandler(this.BusWebFormE_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_beginRefresh;
        private System.Windows.Forms.Button button_gotoHome;
        private System.Windows.Forms.Button button_manualRefresh;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_lastTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_uploadCnt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_failNotice;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private CYControls.RectangleShape rectangleShape1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox refreshInterval;
        private System.Windows.Forms.Timer timer10;
    }
}