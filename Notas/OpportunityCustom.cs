using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoPlugins
{
    public class OpportunityCustom : PluginImplementation
    {  
        
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            //ConnectionFactory conexao = new ConnectionFactory();
            //IOrganizationService service = conexao.GetCrmService();

            Entity opportunity = (Entity)Context.InputParameters["Target"];

           // if(service == null)
                throw new InvalidPluginExecutionException("Conexão NÂO REALIZADA ");



            //CriarOportunidade(opportunity);
        }

        public void CriarOportunidade(Entity oportunidade)
        {

            //IOrganizationService service = crmDynamics2.GetCrmService();

            Entity novaOpportunity = new Entity("opportunity");

            //string nome = oportunidade["name"].ToString();
            //novaOpportunity["name"] = nome;

            //EntityReference moeda = oportunidade.Contains("transactioncurrencyid") ? (EntityReference)oportunidade["transactioncurrencyid"] : null;
            //novaOpportunity["transactioncurrencyid"] = moeda;

            //service.Create(oportunidade);

        }
    }
}
