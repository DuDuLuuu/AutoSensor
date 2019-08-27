using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoSensor
{
    public partial class NewVersionTips : Form
    {
        public SoftUpdateResultData data { get; set; }
        public NewVersionTips()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(data.downloadUrl);  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewVersionTips_Shown(object sender, EventArgs e)
        {
            label1.Text = data.updateDesc;
            this.Text = "发现新的版本:" + data.versionDesc;
        }
    }
}
