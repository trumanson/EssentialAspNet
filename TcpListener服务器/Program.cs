using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpListener服务器
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Loopback;

            IPEndPoint endPoint = new IPEndPoint(address, 49159);

            TcpListener newserver = new TcpListener(endPoint);

            newserver.Start();

            Console.WriteLine("开始监听。。。");

            while (true)
            {
                TcpClient newclient = newserver.AcceptTcpClient();

                Console.WriteLine("已建立连接。");

                NetworkStream ns = newclient.GetStream();

                Encoding utf8 = Encoding.UTF8;

                byte[] requestbuffer = new byte[4096];
                int length = ns.Read(requestbuffer, 0, 4096);

                string requestString = utf8.GetString(requestbuffer, 0, length);

                Console.WriteLine(requestString);

                //返回的状态行
                string statusLine = "Http/1.1 200 OK\r\n";
                byte[] statusLineBytes = utf8.GetBytes(statusLine);

                //返回的内容
                string responseBody = "<html><head><title>From Socket Server</title></head><body><h1>Hello,world.</h1></body></html>";
                byte[] responseBodyBytes = utf8.GetBytes(responseBody);

                //返回的头部
                string responseHeader = string.Format("Content-Type:text/heml;charset=UTF-8\r\nContent-Length:{0}\r\n", responseBody.Length);
                byte[] responseHeaderBytes = utf8.GetBytes(responseHeader);

                ns.Write(statusLineBytes, 0, statusLineBytes.Length);
                ns.Write(responseHeaderBytes, 0, responseHeaderBytes.Length);
                ns.Write(responseBodyBytes, 0, responseBodyBytes.Length);

                newclient.Close();

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
            newserver.Stop();
        }
    }
}
