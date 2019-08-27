using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AutoSensor
{
    public partial class ChooseStartOrder : Form
    {
        public ChooseStartOrder()
        {
            InitializeComponent();
        }
        public delegate void NoticeSelectOrder(string order);
        public NoticeSelectOrder selectEvent;
        public ArrayList dataList { get; set; }
        public ArrayList snList { get; set; }
        
        private void ChooseStartOrder_Shown(object sender, EventArgs e)
        {
            for(int i = dataList.Count - 1; i >= 0; i--){
                listBox1.Items.Add(dataList[i]);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否确认从" + dataList[listBox1.SelectedIndex]+"开始刷新", "再次确认", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
                selectEvent(snList[listBox1.SelectedIndex].ToString());
            }
        }
    }
}
