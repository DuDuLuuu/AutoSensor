using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections;
using ZW.Common;
using AutoSensor.domain;
using AutoSensor.Util;

namespace AutoSensor
{
    public partial class BusWebFormE : Form
    {
        private static string loginUrl = "https://auth.alipay.com/login/index.htm?goto=https://enterpriseportal.alipay.com/index.htm";
        private static string homeUrl = "https://mrchportalweb.alipay.com/user/home.htm#/";
        private static string billUrl = "https://mbillexprod.alipay.com/enterprise/fundAccountDetail.htm";
        private int currentPage = 1;
        private int endPage = 999;
        ArrayList billDatas = null;
        public static string serial = "";
        public bool isTimerZFBStope = true;
        private string collectionAccount = "";
        private bool isFilered = false;
        private int errorCount = 0;
        private static string logPath = Application.StartupPath + "\\logFile";

        public BusWebFormE()
        {
            InitializeComponent();
            billDatas = new ArrayList();
        }

        private void BusWebFormE_Load(object sender, EventArgs e)
        {
            log("打开程序");
            rectangleShape1.SendToBack();
            timer10.Stop();
            //webBrowser.ScriptErrorsSuppressed = true;
            //支付宝
            webBrowser.Navigate("https://www.alipay.com");

        }

        private void webBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            string url = ((WebBrowser)sender).StatusText;
            webBrowser.Navigate(url);
            e.Cancel = true;

        }


        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            log(webBrowser.Url);

            if (webBrowser.Url.Equals(homeUrl))
            {
                webBrowser.Navigate(billUrl);
            }

            if (webBrowser.Url.Equals(loginUrl))
            {
                timer10.Stop();
                log("session过期,关闭自动");
                button_Stop_Click(sender, e);
                System.Environment.Exit(0);
            }


        }


        //开始自动刷新
        private void button_beginRefresh_Click(object sender, EventArgs e)
        {
            log("开始自动刷新");
            try
            {
                int time = int.Parse(refreshInterval.Text.Trim());
                if (time < 5)
                {
                    MessageBox.Show("时间间隔不能太短");
                    return;
                }
                refreshTimeInterval = time;
            }
            catch
            {
                MessageBox.Show("时间间隔格式不对");
                return;
            }
            rectangleShape1.BringToFront();
            if (!webBrowser.Url.Equals(billUrl))
            {
                rectangleShape1.SendToBack();
                MessageBox.Show("请登录支付宝并打开页面到所有交易记录页面");
                return;
            }
            //getAccountName();
            //if (collectionAccount != null && collectionAccount != "") {
            //    serial = BizHelper.getLastOrder(collectionAccount);
            //}

            try
            {
                getAccountName();
                serial = BizHelper.getLastOrder(collectionAccount);
                log("最后一条流水号："+serial);
            }
            catch (Exception a)
            {
                MessageBox.Show("连接服务器失败:" + Constants.serverUrl);
                log("连接服务器失败:" + Constants.serverUrl+" 关闭程序");
                System.Environment.Exit(0);
                return;
            }


            timer10.Start();
            button_beginRefresh.Enabled = false;
        }



        private void button_manualRefresh_Click(object sender, EventArgs e)
        {
            button_Stop_Click(sender, e);
            webBrowser.Navigate(webBrowser.Url);
        }



        /**
         * 爬取数据
         * */
        public bool getDatas()
        {
            string html = "";
            foreach (HtmlElement el in webBrowser.Document.GetElementsByTagName("div"))
                if (el.GetAttribute("id") == "root")
                {
                    html = el.InnerHtml;
                    break;
                }

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var nullValNode = doc.DocumentNode.SelectNodes("//div[@class='emptyText___2-vw-']");
            var tbodyNodes = doc.DocumentNode.SelectNodes("//tbody[@class='ant-table-tbody']");
            //if (nullValNode != null) {
            //    if (nullValNode[0].InnerText.Equals("暂无记录，请更换条件重新搜索"))
            //    {
            //        return true;
            //    }
            //    else {
            //        if (tbodyNodes == null)
            //        {
            //            log("页面错误,获取数据异常");
            //            errorCount++;
            //            return false;
            //        }
            //    }
            //}
            if (nullValNode == null && tbodyNodes == null) {
                errorCount++;
                log("页面错误,获取数据异常,errorCount="+errorCount);
                return false;
            }
            if (nullValNode != null && nullValNode[0].InnerText.Equals("暂无记录，请更换条件重新搜索")) {
                return true;
            }
            if (tbodyNodes == null) {
                return false;
            }

            var accountChildNode = tbodyNodes[0].ChildNodes;
            for (int i = 0; i < accountChildNode.Count; i++)
            {
                getNodeInfo(accountChildNode[i]);
                if (errorCount > 0) {
                    errorCount--;
                }
            }
            return true;
        }

        /**
         * 解析数据节点
         * */
        private void getNodeInfo(HtmlAgilityPack.HtmlNode node)
        {
            int index = 0;
            //时间节点
            var timeNode = node.ChildNodes[index++];
            var timeContent = timeNode.InnerText;
            var timeStr = timeContent.Substring(timeContent.IndexOf("201"));
            //log("时间：" + timeStr);
            //流水号节点
            var serialNode = node.ChildNodes[index++];
            var outContent = serialNode.OuterHtml;
            //坑爹玩意 有的流水号不是32位
            string result = outContent.Substring(outContent.IndexOf("title") + 7, 32);
            result = System.Text.RegularExpressions.Regex.Replace(result, @"[^0-9]+", "");
            var serialStr = result;
            //log("流水号：" + serialStr);
            //商户订单号节点
            var orderNode = node.ChildNodes[index++];
            var orderContent = orderNode.OuterHtml;
            var orderStr = "--";
            if (!orderNode.InnerText.Equals("--"))
            {
                orderStr = orderContent.Substring(orderContent.IndexOf("title") + 7, 23);
            }
            //log("商户订单号：" + orderStr);

            //对方信息节点
            var customerInfoNode = node.ChildNodes[index++];
            var customerInfoStr = customerInfoNode.InnerText;
            //log("对方信息：" + customerInfoStr);
            //账务类型节点
            var typeNode = node.ChildNodes[index++];
            var typeStr = typeNode.InnerText;
            //log("账务类型：" + typeStr);
            //收支金额节点
            var amountNode = node.ChildNodes[index++];
            var amountStr = amountNode.InnerText;
            //log("收支金额：" + amountStr);
            //账户余额节点
            var balanceNode = node.ChildNodes[index++];
            var balanceStr = balanceNode.InnerText;
            //log("账户余额：" + balanceStr);
            //操作节点

            domain.Bill bill = new domain.Bill()
            {
                Time = timeStr,
                Serial = serialStr,
                Order = orderStr,
                CustomerInfo = customerInfoStr,
                Type = typeStr,
                AmountNode = amountStr,
                BalanceNode = balanceStr,
                CollectionAccount = collectionAccount
            };
            billDatas.Add(bill);
            var rs = JSON.stringify(bill);
            //log("......................" + rs);
        }
        //获取当前页数据并跳转至下一页
        //public void getAllDatas()
        //{
        //    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        //    string html = "";
        //    foreach (HtmlElement el in webBrowser.Document.GetElementsByTagName("div"))
        //        if (el.GetAttribute("id") == "root")
        //        {
        //            html = el.InnerHtml;
        //            break;
        //        }
        //    doc.LoadHtml(html);

        //    //开始采集数据
        //    getDatas();
        //    HtmlElement nextBtn = hasNext();
        //    if (nextBtn != null)
        //    {
        //        log("有下一页");
        //        nextClick(nextBtn);
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        //是否有下一页
        private HtmlElement hasNext()
        {
            HtmlElement rs = null;
            HtmlElementCollection links = webBrowser.Document.GetElementsByTagName("a");
            //HtmlElement firstPage = null;
            foreach (HtmlElement link in links)
            {
                if (link.Parent.GetAttribute("title") == "下一页" && link.Parent.GetAttribute("aria-disabled") == "false")
                {
                    rs = link;
                }
                else
                {
                    endPage = currentPage;
                }
            }
            return rs;
        }
        //跳转下一页
        private void nextClick(object next)
        {
            var n = (HtmlElement)next;
            n.InvokeMember("click");
            currentPage++;
        }



        //过滤出要发送的数据:数据集没有包含key返回false
        private bool FilterDatas(ArrayList datas, ArrayList rs, string serial)
        {
            isFilered = false;
            bool isKeyInDatas = false;
            foreach (Bill bill in datas)
            {
                if (bill.Serial != serial)
                {
                    rs.Add(bill);
                }
                else
                {
                    isKeyInDatas = true;
                    break;
                }
            }
            isFilered = true;
            return isKeyInDatas;
        }


        private void sendDatas(ArrayList datas)
        {
            while (!isFilered)
            {

            }
            log("需要发送的数据数量为：" + datas.Count);
            if (datas.Count == 0)
            {
                log("本次没有需要发送的数据");
                billDatas = new ArrayList();
                return;
            }
            datas.Reverse();

            bool sendOver = true;
            //倒序发送，直到容器空了
            foreach (Bill bill in datas)
            {
                bool rs = BizHelper.succNotice(bill);
                //发送成功且服务端更新了最后一条流水号
                if (rs == true && bill.Serial.Equals(serial))
                {
                    continue;
                }
                else
                {
                    sendOver = false;
                    log("发送失败");
                    //清空
                    datas = new ArrayList();
                    break;
                }
            }

            if (sendOver)
            {
                log("当前数据发送完成");
                //清空容器
                billDatas = new ArrayList();
            }
        }

        //最近30天
        private void thirtyDay()
        {
            HtmlElementCollection divs = webBrowser.Document.GetElementsByTagName("div");
            HtmlElement tDay = null;
            foreach (HtmlElement div in divs)
            {

                if (div.InnerText == "最近30天")
                {
                    tDay = div;
                    break;
                }
            }
            if (tDay != null)
            {
                tDay.Focus();
                tDay.InvokeMember("click");
            }
        }
        private void button_Stop_Click(object sender, EventArgs e)
        {
            log("停止自动刷新");
            step = 0;
            timer10.Stop();
            rectangleShape1.SendToBack();
            billDatas = new ArrayList();
            button_beginRefresh.Enabled = true;
        }

        private void BusWebFormE_FormClosing(object sender, FormClosingEventArgs e)
        {
            log("关闭程序");
            System.Environment.Exit(0);
        }

        private void button_gotoHome_Click(object sender, EventArgs e)
        {

            button_Stop_Click(sender, e);
            webBrowser.Navigate(homeUrl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button_Stop_Click(sender, e);

            webBrowser.Navigate(billUrl);
        }
        private void getAccountName()
        {
            HtmlElementCollection divs = webBrowser.Document.GetElementsByTagName("a");
            foreach (HtmlElement div in divs)
            {
                if (div.GetAttribute("id") == "J_nickNameUrl2")
                {
                    collectionAccount = div.GetAttribute("title");
                    log("..............当前账号名称： " + collectionAccount);
                }
            }
        }
        public void sleepRandom()
        {
            Random reum = new Random();
            int sleepTime = reum.Next(7);
            Thread.Sleep(sleepTime * 1000);
        }

        //日志
        public static void log(object val)
        {
            try
            {
                Console.WriteLine(DateTime.Now + " log " + val);
                File.AppendAllText(logPath, contents: DateTime.Now + " log " + val + "\r\n");
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

        }
        // 时间戳转为C#格式时间
        private DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }
        // DateTime时间格式转换为Unix时间戳格式
        private int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        private int step = 0;
        private int lastRefreshTime = 0;
        private int refreshTimeInterval = 300;//间隔时间300秒=5分钟
        private void timer10_Tick(object sender, EventArgs e)
        {
            timer10.Stop();
            int nowTimeStamp = DateTimeToStamp(DateTime.Now);
            int interval = nowTimeStamp - lastRefreshTime;
            string stepVal = "";
            switch (step)
            {
                case 0:
                    stepVal = "刷新(如果间隔时间>=指定刷新间隔)";
                    break;
                case 1:
                    stepVal = "获取本页数据";
                    break;
                case 2:
                    stepVal = "发送数据，归零";
                    break;
                default: break;
            }
            if (stepVal.Equals("刷新(如果间隔时间>=指定刷新间隔)") && interval > 20 && interval < refreshTimeInterval - 20) { }
            else{
                log(" 当前正在执行:" + stepVal + " 当前时间:" + StampToDateTime(nowTimeStamp.ToString()) + " 上次刷新时间：" + StampToDateTime(lastRefreshTime.ToString()) + " 时间间隔：" + interval + "秒 刷新间隔：" + refreshTimeInterval + "秒");
            }
            bool flag = true;
            switch (step)
            {
                case 0://刷新
                    if (interval >= refreshTimeInterval)
                    {
                        lastRefreshTime = nowTimeStamp;
                        flag = doRefresh();
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 1://获取本页数据
                    flag = doGetData();
                    break;
                case 2://发送数据，归零
                    flag = doSendMessage();
                    break;
                default: break;
            }
            if (flag)
            {
                //if (step == 5)
                if (step == 2)
                {
                    step = 0;
                }
                else
                {
                    step++;
                }
            }
            sleepRandom();
            timer10.Start();
        }
        //刷新
        public bool doRefresh()
        {
            bool rs = true;
            try
            {
                webBrowser.Refresh();
            }
            catch (Exception e)
            {
                rs = false;
            }
            return rs;
        }
        //获取当前页数据
        public bool doGetData()
        {
            bool rs = true;
            try
            {
                checkURL();
                rs=getDatas();
            }
            catch (Exception e)
            {
                rs = false;
            }
            return rs;
        }

        //过滤 发送 清空
        public bool doSendMessage()
        {
            bool rs = true;
            try
            {
                ArrayList send = new ArrayList();
                bool flag = FilterDatas(billDatas, send, serial);
                if (flag)
                {
                    log("最近30天前2页，找到最后一条流水号：" + serial);
                }
                else
                {
                    log("最近30天前2页，没找到最后一条流水号：" + serial);
                }
                sendDatas(send);

            }
            catch (Exception e)
            {
                rs = false;
            }
            return rs;
        }
        bool firstError5 = true;//第一次出现errorCount=5
        public void checkURL()
        {
            if (!webBrowser.Url.Equals(billUrl))
            {
                log("session过期,关闭自动");
                System.Environment.Exit(0);
                return;
            }
            if (firstError5&&errorCount == 5) {
                firstError5 = false;
                step=0;
                log("获取数据多次异常,尝试重新刷新");

            }
            if (errorCount > 7) {
                savePicture();
                log("任务多次出现异常,关闭自动");
                System.Environment.Exit(0);
                return;
            }

        }
        public void savePicture() {
            Camera c = new Camera();
            string pictureName = Application.StartupPath + "//" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss")+".jpg";
            Console.WriteLine(pictureName);
            c.CaptureScreen(pictureName);
        }

    }
}
