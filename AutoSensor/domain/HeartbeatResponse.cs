using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDb.Service
{
    [Serializable]
    class HeartbeatResponse
    {
        //时间戳
        private string time;

        public string Time { get => time; set => time = value; }
    }
}
