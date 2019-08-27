using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoSensor.domain
{
    [Serializable]
    public class Bill
    {
        //时间
        private string time;
        //流水
        private string serial;
        //商户订单号
        private string order;
        //对付节点信息
        private string customerInfo;
        //账务类型
        private string type;
        //收支金额
        private string amountNode;
        //账户余额
        private string balanceNode;
        //收款账号
        private string collectionAccount;

        public string Time { get => time; set => time = value; }
        public string Serial { get => serial; set => serial = value; }
        public string Order { get => order; set => order = value; }
        public string CustomerInfo { get => customerInfo; set => customerInfo = value; }
        public string Type { get => type; set => type = value; }
        public string AmountNode { get => amountNode; set => amountNode = value; }
        public string BalanceNode { get => balanceNode; set => balanceNode = value; }
        public string CollectionAccount { get => collectionAccount; set => collectionAccount = value; }

        public Bill()
        {
        }

        public override string ToString()
        {

            return "time=" + this.time + "&serial=" + this.serial + "&order=" + this.order + "&customerInfo=" + this.customerInfo + "&type=" + this.type + "&amountNode=" + this.amountNode + "&balanceNode=" + this.balanceNode + "&collectionAccount=" + this.collectionAccount;
        }
    }
}
