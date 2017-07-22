using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain应用程序域
{
    class Program
    {
        static void Main(string[] args)
        {
            var domain = AppDomain.CreateDomain("MyAppDomain");
            domain.Load("Model");

            foreach (var assembly in domain.GetAssemblies())
            {
                Console.WriteLine(string.Format("{0}\n----------------------------",
                    assembly.FullName));
            }

            Console.ReadKey();
        }
    }
}
