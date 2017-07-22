using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListener服务器
{
    class Program
    {
        static void Main(string[] args)
        {
            //检查系统是否支持
            if (!HttpListener.IsSupported)
            {
                throw new InvalidOperationException("使用HttpListener必须为Windows XP SP2或Sercer 2003以上的系统！");
            }
            string[] prefixes = new string[] { "http://localhost:49159/" };

            HttpListener listener = new HttpListener();

            //增加监听的前缀
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }

            //开始监听
            listener.Start();
            Console.WriteLine("监听中...");
            while (true)
            {
                HttpListenerContext context = listener.GetContext();

                //获取请求对象
                HttpListenerRequest request = context.Request;
                Console.WriteLine("{0}{1} HTTP/1.1", request.HttpMethod, request.RawUrl);
                Console.WriteLine("Accept:{0}", string.Join(",", request.AcceptTypes));
                Console.WriteLine("Accept-Language:{0}", string.Join(",", request.UserLanguages));
                Console.WriteLine("User-Agent:{0}", request.UserAgent);
                Console.WriteLine("Accept-Encoding", request.ContentEncoding);
                Console.WriteLine("Connection:{0}", request.KeepAlive ? "Keep-Alive" : "close");
                Console.WriteLine("Host:{0}", request.UserHostName);
                Console.WriteLine("Pragma:{0}", request.Headers["Pragma"]);

                //取得回应对象
                HttpListenerResponse response = context.Response;

                //构造回应对象
                string responseString
                    = @"<html><head><title>From HttpListener Server</title></head><body><h1>Hello,world.</h1></body></html>";

                response.ContentLength64 = Encoding.UTF8.GetByteCount(responseString);
                response.ContentType = "text/html;charset=UTF-8";

                Stream output = response.OutputStream;
                StreamWriter writer = new StreamWriter(output);
                writer.Write(responseString);
                writer.Close();


                if (Console.KeyAvailable)
                {
                    break;
                }
            }

            listener.Stop();
        }
    }
}
