using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EssentialAspNet
{
    class Program
    {
        static void Main(string[] args)
        {
            //IPAddress
            //取得本机的loopback网络地址，即127.0.0.1
            IPAddress address = IPAddress.Loopback;

            //创建可以访问的端点。端口如果设置为0，表示使用一个空闲的端口号
            IPEndPoint endPoint = new IPEndPoint(address, 49159);

            //创建socket，使用IPv4地址，传输控制协议TCP，双向 可靠 基于连接的字节流
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //将socket绑定到一个端点上
            socket.Bind(endPoint);

            //设置连接队列的长度
            socket.Listen(10);

            Console.WriteLine("开始监听，端口号：{0}", endPoint.Port);
            while (true)
            {
                //开始监听
                Socket client = socket.Accept();

                //输出客户端的地址
                Console.WriteLine(client.RemoteEndPoint);

                //准备读取客户端请求的数据
                byte[] buffer = new byte[4096];

                //接收数据
                int length = client.Receive(buffer, 4096, SocketFlags.None);

                //将请求的数据翻译为UTF-8
                Encoding utf8 = Encoding.UTF8;
                string requestString = utf8.GetString(buffer, 0, length);

                Console.WriteLine(requestString);

                //回应的状态行
                string statusLine = "http/1.1 200 OK\r\n";
                byte[] statusLineBytes = utf8.GetBytes(statusLine);

                string responseBody = "<html><head><title>From Socket Server</title></head><body><h1>Hello,world.</h1></body></html>";

                byte[] responseBodyBytes = utf8.GetBytes(responseBody);

                //回应的头部
                string responseHeader = string.Format("Content-Type:text/html;charset=UTF-8\r\nContent-length:{0}\r\n", responseBody.Length);

                byte[] responseHeaderBytes = utf8.GetBytes(responseHeader);

                //向客户端发送状态信息
                client.Send(statusLineBytes);

                //向客户端发送回应头
                client.Send(responseHeaderBytes);

                //头部与内部的分割行
                client.Send(new byte[] { 13, 10 });

                //向客户端发送内容部分
                client.Send(responseBodyBytes);

                client.Close();
                if (Console.KeyAvailable)
                    break;
            }
            socket.Close();
        }
    }
}
