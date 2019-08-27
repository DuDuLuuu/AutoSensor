using JDb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoSensor
{
    static class Program
    {



        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            HttpListener httpListenner;
            httpListenner = new HttpListener();
            httpListenner.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            //操作系统：管理员运行cmd，执行netsh http add urlacl url=http://+:8080/ user=Everyone
            httpListenner.Prefixes.Add("http://+:" + Constants.heartPort + "/");
            httpListenner.Start();

            new Thread(new ThreadStart(delegate
            {
                try
                {
                    loop(httpListenner);
                }
                catch (Exception)
                {
                    //httpListenner.Stop();
                }
            })).Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //AuthWindow auth = new AuthWindow();
            //Application.Run(auth);
            //if (auth.isExitAll == false)
            //{

            //}
            BusWebFormE form = new BusWebFormE();
            //Test form = new Test();
            Application.Run(form);
        }
        private static void loop(HttpListener httpListenner)
        {
            while (true)
            {
                HttpListenerContext context = httpListenner.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                Servlet servlet = new HeartbeatServlet();
                servlet.onCreate();
                if (request.HttpMethod == "POST")
                {
                    servlet.onPost(request, response);
                }
                else if (request.HttpMethod == "GET")
                {
                    servlet.onGet(request, response);
                }
                response.Close();
            }
        }
    }
}
