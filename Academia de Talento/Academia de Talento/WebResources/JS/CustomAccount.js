if (typeof (Treinamento) == "undefined") { Treinamento = {} }
if (typeof (Treinamento.Account) == "undefined") { Treinamento.Account = {} }

Treinamento.Account = {
    new_TipoDaConta: {
        Nacional: 100000000,
        Internacional: 100000001
    },
    Attributtes: {
        NEW_CNPJ: "new_cnpj",
        NEW_TipoDaConta: "new_tipodaconta",
        NEW_TotalDeOportunidades: "new_totaldeoportunidades",
        NEW_ValorTotalDeOportunidades: "new_valortotaldeoportunidades",
        ContatoPrimario: "primarycontactid",
        NEW_DataDaUltimaOportunidade: "new_datadaultimaoportunidadee"
    },
    OnLoad: function (context) {
        this.CNPJOnChange(context);
        this.TipoDaContaOnChange(context);

        var formContext = context.getFormContext();
        formContext.getControl(Treinamento.Account.Attributtes.NEW_TotalDeOportunidades).setDisabled(true);
        formContext.getControl(Treinamento.Account.Attributtes.NEW_ValorTotalDeOportunidades).setDisabled(true);

       // var dataultimaoportunidade = formContext.getAttribute(Treinamento.Account.Attributtes.NEW_DataDaUltimaOportunidade).setValue(new Date());
        // pega o id de onde esta
        var id = Xrm.Page.data.entity.getId();
        // eq = equal "="
        Xrm.WebApi.online.retrieveMultipleRecords("contact", "?$select=contactid,fullname&$filter=_parentcustomerid_value eq " + id + " and fyi_contatoprincipal eq true&$top=1").then(
            function success(results) {
                if (results.entities.length > 0) {
                    var contatoPrimario = [];
                    contatoPrimario[0] = {};
                    contatoPrimario[0].id = results.entities[0].contactid;
                    contatoPrimario[0].name = results.entities[0].fullname;
                    contatoPrimario[0].entityType = "contact";
                    formContext.getAttribute("primarycontactid").setValue(contatoPrimario);
                }
            },
            function (error) {
                DynamicsCustomAlert(error.message, "Erro com a Query de Contatos!");
            }
        );
    },
    CNPJOnChange: function (context) {
        var formContext = context.getFormContext();
        var cnpjField = "new_cnpj";

        var cnpj = formContext.getAttribute(cnpjField).getValue();

        if(cnpj == "" || cnpj == null)
            return;

        cnpj = cnpj.replace(".", "").replace(".", "").replace("/", "").replace("-", "");

        if (cnpj.length != 14) {
            formContext.getAttribute(cnpjField).setValue("");
            this.DynamicsCustomAlert("Por favor digite 14 caracteres no CNPJ", "Erro de Validação CNPJ");
        } else {
            cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");

            var id = Xrm.Page.data.entity.getId();

            Xrm.WebApi.online.retrieveMultipleRecords("account", "?$select=new_cnpj&$filter=new_cnpj eq '" + cnpj + "' and accountid ne " + id).then(
                function success(results) {
                    if (results.entities.length > 0) {
                        formContext.getAttribute(cnpjField).setValue("");
                        DynamicsCustomAlert("Já existe uma conta com este CNPJ no sistema!", "CNPJ Duplicado");
                    }else {
                        formContext.getAttribute(cnpjField).setValue(cnpj);
                    }
                },
                function (error) {
                    DynamicsCustomAlert(error.message, "Error");
                }
            );

            
        }
    },
    TipoDaContaOnChange: function (context) {
        var formContext = context.getFormContext();
        

        var valorCampo = formContext.getAttribute(Treinamento.Account.Attributtes.NEW_TipoDaConta).getValue();

        /* getText = para pegar o nome do campo
        var valorTexto = formContext.getAttribute(Treinamento.Account.Attributtes.NEW_TipoDaConta).getText();
        */

        if (valorCampo == Treinamento.Account.new_TipoDaConta.Nacional) {
            formContext.getAttribute(Treinamento.Account.Attributtes.NEW_CNPJ).setRequiredLevel("required");
            formContext.getControl(Treinamento.Account.Attributtes.NEW_CNPJ).setVisible(true);
        } else {
            if (valorCampo == Treinamento.Account.new_TipoDaConta.Internacional) {
                formContext.getAttribute(Treinamento.Account.Attributtes.NEW_CNPJ).setRequiredLevel("none");
                formContext.getControl(Treinamento.Account.Attributtes.NEW_CNPJ).setVisible(false);
            }
        }
    },
    DynamicsCustomAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };

        var alertOptions = {
            heigth: 120,
            width: 200
        };

        Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
    }
}