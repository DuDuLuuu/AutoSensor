using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AutoSensor
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (StreamReader sr = new StreamReader("data.txt", Encoding.GetEncoding("gbk")))
            {
                string html = sr.ReadToEnd();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                var nodes = doc.DocumentNode.SelectNodes("//tr[@class='J-item ']");
                for (int i = 0; i < nodes.Count; i++)
                {
                    //取标题、序列号
                    var nameChildNode  = nodes[i].SelectSingleNode(nodes[i].XPath+"/td[@class='name']");
                    var titleNode = nameChildNode.SelectSingleNode(nameChildNode.XPath+"/p/a");
                    if (titleNode != null)
                    {
                        var title = titleNode.InnerText;
                    }
                    var sn = nameChildNode.SelectSingleNode(nameChildNode.XPath + "/div/a").Attributes["data-clipboard-text"].Value;
                    //取金额
                    var amountNode = nodes[i].SelectSingleNode(nodes[i].XPath + "/td/span[@class='amount-pay']");
                    if (amountNode != null)
                    {
                        var money = amountNode.InnerText;
                    }
                    var detailNode = nodes[i].SelectSingleNode(nodes[i].XPath + "/td[@class='detail']");
                    //取时间
                    var timeSpanNode = detailNode.SelectSingleNode(detailNode.XPath + "/div//span[@data-type='detail']");
                    if (timeSpanNode != null)
                    {
                        string[] strings = timeSpanNode.Attributes["data-value"].Value.Split(' ');
                        var time = strings[strings.Length - 1];
                    }
                    //取备注
                    var memoSpanNode = detailNode.SelectSingleNode(detailNode.XPath + "/div//span[@data-type='memo']");
                    if (memoSpanNode != null)
                    {
                        var memo = memoSpanNode.Attributes["data-info"].Value;
                    }
                }
            }
        }
    }
}
