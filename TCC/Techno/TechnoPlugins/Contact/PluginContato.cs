using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace TechnoPlugins
{
    public class PluginContato : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {

            Entity contact = (Entity)Context.InputParameters["Target"];


            if (contact.Contains("tc4_cpf"))
            {
                //Retrive CPFs that are equal to the one created
                QueryExpression queryAccount = new QueryExpression("contact");
                queryAccount.ColumnSet.AddColumns("tc4_cpf");
                queryAccount.Criteria.AddCondition("tc4_cpf", ConditionOperator.Equal, contact["tc4_cpf"]);
                EntityCollection allCpfs = Service.RetrieveMultiple(queryAccount);

                if (allCpfs.Entities.Count > 0)
                {
                    throw new InvalidPluginExecutionException("Já existe um contato cadastrado com esse CPF");
                }
            }
        }

    }
}
