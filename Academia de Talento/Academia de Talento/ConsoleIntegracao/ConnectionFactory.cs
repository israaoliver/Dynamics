using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegracao
{
    class ConnectionFactory
    {
        public static IOrganizationService GetCrmService()
        {
            string connectionString =
                "AuthType=OAuth;" +
                "Username=IsraaOliveira@IsraaOliveira.onmicrosoft.com;" +
                "Password=Dre@mer27;" +
                "Url=https://academia39.crm2.dynamics.com/;" +
                "AppId=305d76f7-c1b4-4aef-bb43-67a9111bf276;" +
                "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;
        }
    }
}
