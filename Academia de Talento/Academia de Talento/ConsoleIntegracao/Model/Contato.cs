using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegracao.Model
{
    class Contato
    {
        public IOrganizationService Service { get; set; }

        public string TableName = "contact";

        public Contato(IOrganizationService service)
        {
            this.Service = service;
        }

        public EntityCollection ContatoByAccount(Guid accountId)
        {
            QueryExpression queryContats = new QueryExpression(this.TableName);
            queryContats.ColumnSet.AddColumns("fullname", "emailaddress1");
            queryContats.Criteria.AddCondition("parentcustomerid", ConditionOperator.Equal, accountId);

            queryContats.AddLink("account", "parentcustomerid", "accountid", JoinOperator.Inner);
            queryContats.LinkEntities[0].Columns.AddColumns("telephone1", "new_tipodaconta","new_totaldeoportunidades","new_valortotaldeoportunidades");
            queryContats.LinkEntities[0].EntityAlias = "conta";


            return this.Service.RetrieveMultiple(queryContats);


        }

        public object RetrieveMultipleContactByAccountLinq(Guid accountid)
        {
            var context = new OrganizationServiceContext(this.Service);

            var resultado = (from contact in context.CreateQuery("contact")
                             join account in context.CreateQuery("account")
                                          on contact["parentcustumerid"] equals account["accountid"]
                             where ((EntityReference)contact["parentcustumerid"]).Id == accountid
                             select new
                             {
                                 Name = contact["fullname"].ToString(),
                                 Email = contact.Contains("emailaddress1") ? contact["emailaddress1"].ToString() : string.Empty
                             }).ToList(); 

            foreach(var contact in resultado)
            {
                Console.WriteLine(contact.Name);
                Console.WriteLine(contact.Email);
            }

            return resultado;
        }
    }
}
