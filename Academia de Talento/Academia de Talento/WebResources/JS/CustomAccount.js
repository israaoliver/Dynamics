if (typeof (Treinamento) == "undefined") { Treinamento = {} }
if (typeof (Treinamento.Account) == "undefined") { Treinamento.Account = {} }

Treinamento.Account = {
    OnLoad: function (context) {
        this.CNPJOnChange(context);
    },
    CNPJOnChange: function (context) {
        var formContext = context.getFormContext();
        var cnpjField = "new_cnpj";

        var cnpj = formContext.getAttribute(cnpjField).getValue();
        cnpj = cnpj.replace(".", "").replace(".", "").replace("/", "").replace("-", "");

        if (cnpj.length != 14) {
            formContext.getAttribute(cnpjField).setValue("");
            this.DynamicsCustomAlert("Por favor digite 14 caracteres no CNPJ", "Erro de Validação CNPJ");
        } else {
            cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
            formContext.getAttribute(cnpjField).setValue(cnpj);
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