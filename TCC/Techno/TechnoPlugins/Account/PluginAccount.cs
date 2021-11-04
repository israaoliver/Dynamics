using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace TechnoPlugins
{
    public class PluginAccount : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {

            Entity account = (Entity)Context.InputParameters["Target"];


            if (account.Contains("tc4_cnpj"))
            {

                QueryExpression queryAccount = new QueryExpression("account");
                queryAccount.ColumnSet.AddColumns("tc4_cnpj");
                queryAccount.Criteria.AddCondition("tc4_cnpj", ConditionOperator.Equal, account["tc4_cnpj"]);
                EntityCollection allCnpjs = Service.RetrieveMultiple(queryAccount);

                if (allCnpjs.Entities.Count > 0)
                {
                    throw new InvalidPluginExecutionException("Já existe uma conta cadastrada com esse CNPJ");
                }
            }
        }

    }
}