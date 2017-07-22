using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 进程
{
    class Program
    {
        static void Main(string[] args)
        {
            //Process process = Process.Start("notepad.exe");

            //Thread.Sleep(1000 * 10);

            //process.Kill();

            //var processList = Process.GetProcesses().OrderBy(x => x.Id);

            //foreach (var process in processList)
            //{
            //    Console.WriteLine(string.Format("ProcessId is :{0}\t ProcessName:{1}", process.Id, process.ProcessName));

            //}
            //Console.ReadKey();

            var process = Process.GetCurrentProcess();

            var modules = process.Modules;

            foreach (ProcessModule module in modules)
                Console.WriteLine(string.Format("{0}\n  URL:{1}\n  Version:{2}",
                    module.ModuleName, module.FileName, module.FileVersionInfo.FileVersion));
            Console.ReadKey();
        }
    }
}
