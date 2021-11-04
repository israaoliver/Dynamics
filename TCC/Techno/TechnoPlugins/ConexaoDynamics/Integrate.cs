using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;


namespace TechnoPlugins
{
    public class Integrate
    {
        public static IOrganizationService GetCrmService()
        {
            string connectionString =
                "AuthType=OAuth;" +
                "Username=dyn@techno1marketplace.onmicrosoft.com;" +
                "Password=Fyi@2021;" +
                "Url=https://orgd13880b8.crm2.dynamics.com;" +
                "AppId=644535b3-cf67-4d65-aae4-39c82a105fef;" +
                "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;
        }
    }
}