using Microsoft.Xrm.Sdk;

namespace ConsoleIntegracao.Model
{
    internal class IOrganizationServiceContext
    {
        private IOrganizationService service;

        public IOrganizationServiceContext(IOrganizationService service)
        {
            this.service = service;
        }
    }
}