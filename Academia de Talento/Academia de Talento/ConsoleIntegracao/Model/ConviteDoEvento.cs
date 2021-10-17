using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegracao.Model
{
    class ConviteDoEvento
    {

        public IOrganizationService Service { get; set; }

        public ConviteDoEvento(IOrganizationService service)
        {
            this.Service = service;        }

        public void ExecuteMultipleRequestConviteDoEvento()
        {
            ExecuteMultipleRequest executeMultipleRequest = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = true,
                    ReturnResponses = false
                }
            };

            for (int i = 0; i < 10; i++)
            {
                Entity conviteDoEvento = new Entity("new_convitedoevento");
                conviteDoEvento["new_evento"] = new EntityReference("new_evento", new Guid("a36a9a18-132e-ec11-b6e6-00224837b996"));
                conviteDoEvento["new_cliente"] = new EntityReference("account", new Guid("e07030ea-2b26-ec11-b6e6-0022483722e5"));

                CreateRequest createRequest = new CreateRequest()
                {
                    Target = conviteDoEvento
                };

                executeMultipleRequest.Requests.Add(createRequest);
            }

            ExecuteMultipleResponse executeMultipleResponse = (ExecuteMultipleResponse)this.Service.Execute(executeMultipleRequest);

            foreach (var res in executeMultipleResponse.Responses)
            {
                if (res.Fault != null)
                {
                    Console.WriteLine(res.Fault.ToString());
                }
            }
        }
    }
}

