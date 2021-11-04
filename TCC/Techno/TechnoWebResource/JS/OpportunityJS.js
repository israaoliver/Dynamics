if (typeof (Container) == "undefined") { Container = {} }
if (typeof (Container.Oportunidade) == "undefined") { Container.Oportunidade = {} }

Container.Oportunidade = {

    OPPOnLoad: function (context) {
        var formContext = context.getFormContext();
        var integracao = formContext.getAttribute("tc4_integracao").getValue();

        if (integracao == true) {
            formContext.getControl("name").setDisabled(true);
            formContext.getControl("parentcontactid").setDisabled(true);
            formContext.getControl("parentaccountid").setDisabled(true);
            formContext.getControl("purchasetimeframe").setDisabled(true);
            formContext.getControl("transactioncurrencyid").setDisabled(true);
            formContext.getControl("budgetamount").setDisabled(true);

            formContext.getControl("purchaseprocess").setDisabled(true);
            formContext.getControl("description").setDisabled(true);
            formContext.getControl("msdyn_forecastcategory").setDisabled(true);
            formContext.getControl("currentsituation").setDisabled(true);
            formContext.getControl("customerneed").setDisabled(true);
            formContext.getControl("proposedsolution").setDisabled(true);

        }

    },

}