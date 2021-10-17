using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegracao.Model
{
    class Account
    {
        public string TableName = "account";

        public IOrganizationService Service { get; set; }


        public Account(IOrganizationService service)
        {
            this.Service = service;
        }

        


        public void CreateAccount(IOrganizationService service)
        {

            Entity account = new Entity(this.TableName);
            account["new_cnpj"] = "1234567890132";
            account["name"] = "Conta criada na model";
            account["new_tipodaconta"] = new OptionSetValue(100000000);
            account["new_totaldeoportunidades"] = 1;
            account["new_valortotaldeoportunidades"] = new Money(5000);
            account["primarycontactid"] = new EntityReference("contact", new Guid("df7030ea-2b26-ec11-b6e6-0022483722e5"));

            Guid accountId = service.Create(account);

            Console.WriteLine($"https://academia39.crm2.dynamics.com/main.aspx?appid=2d81c7a0-6a21-ec11-b6e6-00224837a251&forceUCI=1&pagetype=entityrecord&etn=contact&id={accountId}");
            Console.ReadKey();
        }

        public void UpdateAccount()
        {
            Entity account = new Entity(this.TableName);
            account.Id = new Guid("e07030ea-2b26-ec11-b6e6-0022483722e5");
            account["new_cnpj"] = "22.726.332/0002-34";
            account["new_tipodaconta"] = new OptionSetValue(100000001);
            this.Service.Update(account);
        }

        public void DeleteAccount()
        {
            this.Service.Delete(this.TableName, new Guid("80d70cab-3926-ec11-b6e6-0022483722e5"));
        }

    }
      
}
