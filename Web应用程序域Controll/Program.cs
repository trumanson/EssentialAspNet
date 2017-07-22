using Intelligencer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Web应用程序域Controll
{
    class Program
    {
        static void Main(string[] args)
        {
            Type hostType = typeof(Intelligencer1);

            Intelligencer1 intelligencer = ApplicationHost.CreateApplicationHost(hostType, "/", Environment.CurrentDirectory) as Intelligencer1;

            Console.WriteLine("Current Domain ID:{0}\r\n", AppDomain.CurrentDomain.Id);

            Console.WriteLine(intelligencer.Report());

            Console.ReadKey();
        }
    }
}
