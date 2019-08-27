using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZW.Common;
using AutoSensor.domain;
using AutoSensor;
namespace JDb.Service
{
    public class HeartbeatServlet : JDb.Service.Servlet
    {
        public override void onCreate()
        {
            base.onCreate();
        }

        public override void onGet(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine("GET:" + request.Url);
            byte[] buffer = Encoding.UTF8.GetBytes("get OK");

            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
            //listener.Stop();
        }

        public override void onPost(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine("POST:" + request.Url);



            Stream stream = request.InputStream;
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string content = reader.ReadToEnd();

            //心跳
            if ("/heartbeat".Equals(request.Url.AbsolutePath))
            {
                //时间戳
                HeartbeatResponse rs = new HeartbeatResponse();
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                rs.Time = Convert.ToInt64(ts.TotalSeconds).ToString();

                //成功
                byte[] res = Encoding.UTF8.GetBytes(JSON.stringify(rs));
                response.OutputStream.Write(res, 0, res.Length);

            }
        }
    }

}
