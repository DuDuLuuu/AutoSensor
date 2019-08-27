using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZW.Common
{
    class LoggerHelper
    {
        public string fileName;
        public LoggerHelper(string _fileName)
        {
            fileName = _fileName;
        }

        public void info(string txt)
        {
            using (FileStream fs = new FileStream("ep.log", FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    DateTime lastRefreshTIme = DateTime.Now;
                    sw.WriteLine(lastRefreshTIme.ToLongTimeString()+"-"+txt);
                    sw.Flush();
                }
            }
        }

    }
}
