using Microsoft.Xrm.Sdk;
using TechnoConsole.ConnectionsFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            IOrganizationService connectDynamics1 = ConnectionDynamics1.GetCrmService();

            IOrganizationService connectDynamics2 = ConnectionDynamics2.GetCrmService();
        }
    }
}
