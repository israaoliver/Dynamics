using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace TechnoPlugins

{
    public class OpportunityState : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity opportunity = (Entity)Context.InputParameters["Target"];

            Entity notification = new Entity("tc4_notificacaoaocliente");

            if (((OptionSetValue)opportunity["msdyn_forecastcategory"]).Value != 100000005)
            {                
                return;
            }

            QueryExpression queryOpportunity = new QueryExpression("opportunity");
            queryOpportunity.ColumnSet.AddColumns("parentcontactid");
            queryOpportunity.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, opportunity["opportunityid"]);
            EntityCollection retrieveContactId = Service.RetrieveMultiple(queryOpportunity);
            EntityReference parentcontactid = (EntityReference)retrieveContactId[0]["parentcontactid"];            

            Entity contactName = Service.Retrieve("contact", (Guid)parentcontactid.Id, new ColumnSet("fullname"));

            //throw new InvalidPluginExecutionException($"{contactName["fullname"]}");

            DateTime utcDate = DateTime.UtcNow;

            DateTime brasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            notification["tc4_mensagem"] = "Parabéns pela sua nova aquisição, agradecemos a confiança. Volte sempre!";

            notification["tc4_nomedocliente"] = contactName["fullname"];

            notification["tc4_datadanotificacao"] = brasilia;

            Service.Create(notification);
        }
    }
}