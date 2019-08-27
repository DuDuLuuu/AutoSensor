using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSensor.domain
{
    [Serializable]
    class BillResponse
    {
        //上次一次刷新时间
        private long lastRefreshTime;
        //账单
        private Bill bill;
        //刷新间隔时间/秒
        private long RefreshInterval;

        public long LastRefreshTime { get => lastRefreshTime; set => lastRefreshTime = value; }
        public Bill Bill { get => bill; set => bill = value; }
        public long RefreshInterval1 { get => RefreshInterval; set => RefreshInterval = value; }
    }
}
