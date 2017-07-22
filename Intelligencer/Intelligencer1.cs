using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Intelligencer
{
    public class Intelligencer1 : MarshalByRefObject
    {
        public string Report()
        {
            AppDomain domain = AppDomain.CurrentDomain;

            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("Domain ID:{0}\r\n", domain.Id);

            builder.AppendFormat("VirtualPath:{0}\r\n", HostingEnvironment.ApplicationVirtualPath);

            builder.AppendFormat("PhysicalPath:{0}\r\n", HostingEnvironment.ApplicationPhysicalPath);

            return builder.ToString();
        }
    }
}
