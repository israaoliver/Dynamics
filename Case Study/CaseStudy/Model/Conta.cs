using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Model
{
    public class Conta
    {
        public IOrganizationService Service { get; set; }

        public string TableName = "contact";

        public Conta(IOrganizationService service)
        {
            this.Service = service;
        }

        public EntityCollection ContatoByAccount(Guid accountId)
        {
            QueryExpression queryContats = new QueryExpression(this.TableName);
            queryContats.ColumnSet.AddColumns("fullname", "g07_niveldocliente", "contactid");
            queryContats.Criteria.AddCondition("parentcustomerid",ConditionOperator.Equal,accountId);

            queryContats.AddLink("contact", "parentcustomerid", "accountid", JoinOperator.Inner);
            queryContats.LinkEntities[0].EntityAlias = "contato";

            EntityCollection contacts = this.Service.RetrieveMultiple(queryContats);
            return contacts;


        }

        public void AtualizandoDesconto(double ValorDesconto, string usuario)
        {
            IOrganizationService service = ConnectionFactory.GetCrmService();

            Console.WriteLine("Você deseja atualizar essa oportunidade? Y/N");

            var resposta = Console.ReadLine();

            if (resposta == "Y" || resposta == "y")
            {
                Entity contato = new Entity(this.TableName);

                contato.Id = new Guid(usuario);
                contato["g07_discountamount"] = ValorDesconto.ToString();
                this.Service.Update(contato);
                Console.WriteLine("Seu desconto foi atualizado com sucesso");
            }
            if (resposta == "N" || resposta == "n")
            {
                Console.WriteLine("Obrigado pelo consulta volte sempre");
            }
        }
    }
}
