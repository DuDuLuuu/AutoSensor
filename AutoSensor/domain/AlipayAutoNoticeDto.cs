using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSensor.domain
{
    [Serializable]
    class AlipayAutoNoticeDto
    {
        //最后一条流水号
        private string lastestOrder;
        //账号
        private string companyAccount;

        public string LastestOrder { get => lastestOrder; set => lastestOrder = value; }
        public string CompanyAccount { get => companyAccount; set => companyAccount = value; }
    }
}
