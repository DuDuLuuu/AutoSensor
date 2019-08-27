using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Utilities;
using System.Net;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using ZW.Common.Properties;
using ZW.Common;
using ZW.Common.Hardware;


namespace AutoSensor
{
    [DataContract()]
    public class ServiceCommonResult
    {
        [DataMember(Order = 0)]
        public string code { get; set; }
        [DataMember(Order = 1)]
        public string msg { get; set; }
        [DataMember(Order = 2)]
        public string success { get; set; }
    }

    [DataContract()]
    public class ServiceDataResult
    {
        [DataMember(Order = 0)]
        public string code { get; set; }
        [DataMember(Order = 1)]
        public string msg { get; set; }
        [DataMember(Order = 2)]
        public string success { get; set; }
        [DataMember(Order = 3)]
        public string data { get; set; }
    }

    [DataContract()]
    public class SoftUpdateResult
    {
        [DataMember(Order = 0)]
        public string code { get; set; }
        [DataMember(Order = 1)]
        public string msg { get; set; }
        [DataMember(Order = 2)]
        public string success { get; set; }
        [DataMember(Order = 3)]
        public SoftUpdateResultData data { get; set; }
    }

    [DataContract()]
    public class SoftUpdateResultData
    {
        [DataMember(Order = 0)]
        public bool needUpdate { get; set; }
        [DataMember(Order = 1)]
        public string versionDesc { get; set; }
        [DataMember(Order = 2)]
        public int versionNo { get; set; }
        [DataMember(Order = 3)]
        public string updateDesc { get; set; }
        [DataMember(Order = 4)]
        public string downloadUrl { get; set; }
    }

    class BizHelper
    {
        private static string token;
        private static DesProperties prop = new DesProperties();
        public static string lastError = "";

        static BizHelper()
        {
            prop.DesKey = Constants.mykey;
            prop.load("./data.db");
        }

        public static bool autoLogin()
        {
            string username = (string)prop["username"];
            if (username == null)
            {
                return false;
            }
            string pwd = (string)prop["pwd"];
            return login(username, pwd);
        }

        public static bool login(string username, string pwd)
        {
            string posData = "loginName=" + username + "&pwd=" + pwd + "&serno=" + CpuHelper.GetCPUSerialNumber();
            HttpResult httpResult = postData(Constants.serverUrl + "/all/terminal/login", posData);
            try
            {
                ServiceDataResult result = JSON.parse<ServiceDataResult>(httpResult.Html);
                if (result.code == "0")
                {
                    token = result.data;
                    prop.Add("username", username);
                    prop.Add("pwd", pwd);
                    prop.save("./data.db");
                    return true;
                }
                else
                {
                    lastError = result.msg;
                    return false;
                }
            }
            catch
            {
                Console.WriteLine(httpResult.Html);
                return false;
            }
        }
        public static bool heart()
        {
            string posData = "token=" + token;
            HttpResult httpResult = postData(Constants.serverUrl + "/all/terminal/checkStatus", posData);
            ServiceCommonResult result = JSON.parse<ServiceCommonResult>(httpResult.Html);
            if (result.code == "0")
            {
                return true;
            }
            else
            {
                lastError = result.msg;
                return false;
            }
        }


        //发送消息
        public static bool succNotice(domain.Bill bill)
        {
            string posData = bill.ToString();
            HttpResult httpResult = postData(Constants.serverUrl + "/payIntf/terminal/receiveNoticeMsg", posData);
            string jsonStr = subResultHtml(httpResult.Html);
            Console.WriteLine(jsonStr);
            ServiceDataResult result = JSON.parse<ServiceDataResult>(jsonStr);
            if (result.code == "0")
            {
                BusWebFormE.log("发送消息成功： " + JSON.stringify(bill));
                //更新最后一条流水号
                BusWebFormE.serial = result.data;
                return true;
            }
            else
            {
                lastError = result.msg;
                return false;
            }

            //测试
            //Console.WriteLine("发送消息成功： " + JSON.stringify(bill));
            //BusWebFormE.serial = bill.Serial;
            //return true;
        }
        //获取最后一条流水号
        public static string getLastOrder(String collectionAccount)
        {
            string posData = "collectionAccount=" + collectionAccount;
            HttpResult httpResult = postData(Constants.serverUrl + "/payIntf/terminal/sendLatestOrder", posData);
            string jsonStr = subResultHtml(httpResult.Html);
            Console.WriteLine(jsonStr);
            ServiceDataResult result = JSON.parse<ServiceDataResult>(jsonStr);
            Console.WriteLine(result.data);
            if (result.code == "0")
            {
                return result.data;
                //return "6666666666666666666666";
            }
            else
            {
                //lastError = result.msg;

                return null;
            }
            //测试
            //return "20190524200040011100460064225800";
        }



        public static bool uploadOrder(string orderSn,string title,string money,string memo,string time)
        {
            string posData = "p={ token : \"" + token + "\", orderInfos: [ { orderNo:\"" + orderSn + "\", title:\"" + title +
                "\", money: \"" + money + "\", orderMemo:\"" + memo + "\", orderTime: \"" + time + "\"}] }";
            HttpResult httpResult = postData(Constants.serverUrl + "/all/terminal/orderUpload", posData);
            ServiceCommonResult result = JSON.parse<ServiceCommonResult>(httpResult.Html);
            if (result.code == "0")
            {
                return true;
            }
            else
            {
                lastError = result.msg;
                return false;
            }
        }

        public static SoftUpdateResultData queryNewVersion()
        {
            string posData = "softId=" + Constants.softId + "&versionNo=" + Constants.versionNo;
            HttpResult httpResult = postData(Constants.serverUrl + "/all/terminal/checkSoftUpdate", posData);
            SoftUpdateResult result = JSON.parse<SoftUpdateResult>(httpResult.Html);
            if (result.code == "0")
            {
                return result.data;
            }
            else
            {
                lastError = result.msg;
                return null;
            }
        }

        private static HttpResult postData(string url, string postData)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,//URL     必需项
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Method = "Post",
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",//用户的浏览器类型，版本，操作系统     可选项有默认值
                //Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值       
                ResultType = ResultType.String,//返回数据类型，是Byte还是String
                Postdata = postData,
                PostEncoding = System.Text.Encoding.GetEncoding("utf-8")
            };
            item.Header.Add("RPC_SECRET_KEY", "RPC_SECRET_KEY_^#@91zcp~");
            //得到HTML代码
            return http.GetHtml(item);
        }

        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }
        private static string subResultHtml(string html)
        {
            string s = html;
            s = s.Substring(s.LastIndexOf("{"));
            s = s.Substring(0, s.Length - 2);
            s = s.Replace("\\", "");
            return s;
        }
    }
}
