if (typeof (Tcc) == "undefined") { Tcc = {} }
if (typeof (Tcc.Contact) == "undefined") { Tcc.Contact = {} }

Tcc.Contact = {

    CPFOnChange: function (context) {
        var formContext = context.getFormContext();
        var cpffield = "tc4_cpf";

        var cpf = formContext.getAttribute(cpffield).getValue();

        if (cpf == "" || cpf == null)
            return;

        cpf = cpf.replace(".", "").replace(".", "").replace("-", "");

        if (cpf.length != 11) {
            this.DynamicsCustomAlert("Por favor digíte 11 caracteres no campo CPF", "Erro de validação do CPF");
            formContext.getAttribute(cpffield).setValue(null);
        }
        else {
            cpf = cpf.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");

            Xrm.WebApi.online.retrieveMultipleRecords("contact", "?$select=tc4_cpf&$filter=tc4_cpf eq '" + cpf + "'").then(
                function success(results) {
                    if (results.entities.length > 0) {
                        formContext.getAttribute(cpffield).setValue("");
                        Tcc.Contact.DynamicsCustomAlert("Já existe um contato cadastrado com este CPF no sistema", "CPF Duplicado!");
                    }
                    else {
                        formContext.getAttribute(cpffield).setValue(cpf);
                    }
                },
                function (error) {
                    Tcc.Contact.DynamicsCustomAlert(error.message, "Error");
                }
            );
        }
    },

    CEPOnChange: function (context) {
        var formContext = context.getFormContext();
        var cepfield = "address1_postalcode";

        var cep = formContext.getAttribute(cepfield).getValue();
        cep = cep.replace("-", "");

        if (cep.length != 8) {
            this.DynamicsCustomAlert("Por favor digíte 8 caracteres no campo CEP", "Erro de validação do CEP");
            formContext.getAttribute(cepfield).setValue(null);
        }
        else {
            cep = cep.replace(/^(\d{5})(\d{3})/, "$1-$2");
            formContext.getAttribute(cepfield).setValue(cep);
        }

    },

    DynamicsCustomAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "Ok",
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