using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoPlugins2.Infrastructure;

namespace TechnoPlugins2
{
    public class ConnectToDynamics : PluginImplement
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            IOrganizationService service = ConnectionFactory.GetCrmService();

            if (service != null)
                throw new InvalidPluginExecutionException("CONNECTED");
            else
                throw new InvalidPluginExecutionException("ERROR");
        }
    }
}
