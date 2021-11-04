if (typeof (Tcc) == "undefined") { Tcc = {} }
if (typeof (Tcc.Account) == "undefined") { Tcc.Account = {} }

Tcc.Account = {

    TC4_NivelDeCliente: {
        Silver: 100000000,
        Gold: 100000001,
        Platinum: 100000002
    },
    TC4_Porte: {
        Pequeno: 100000000,
        Medio: 100000001,
        Grande: 100000002
    },

    CNPJOnChange: function (context) {
        var formContext = context.getFormContext();
        var cnpjfield = "tc4_cnpj";

        var cnpj = formContext.getAttribute(cnpjfield).getValue();

        if (cnpj == "" || cnpj == null)
            return;

        cnpj = cnpj.replace(".", "").replace(".", "").replace("/", "").replace("-", "");

        if (cnpj.length != 14) {
            this.DynamicsCustomAlert("Por favor digíte 14 caracteres no campo CNPJ", "Erro de validação do CNPJ");
            formContext.getAttribute(cnpjfield).setValue(null);
        }
        else {
            cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");

            //var id = Xrm.Page.data.entity.getId();

            Xrm.WebApi.online.retrieveMultipleRecords("account", "?$select=tc4_cnpj&$filter=tc4_cnpj eq '" + cnpj + "'").then(
                function success(results) {
                    if (results.entities.length > 0) {
                        formContext.getAttribute(cnpjfield).setValue("");
                        Tcc.Account.DynamicsCustomAlert("Já existe uma conta cadastrada com este CNPJ no sistema", "CNPJ Duplicado!");
                    }
                    else {
                        formContext.getAttribute(cnpjfield).setValue(cnpj);
                    }
                },
                function (error) {
                    Tcc.Account.DynamicsCustomAlert(error.message, "Error");
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
    },
        
        
    NameOnChange: function (context) {
        function convertFirstCharacterToUppercase(nome) {
            var primeiraLetra = nome.substring(0, 1);
            var restoNome = nome.substring(1);

            return primeiraLetra.toUpperCase() + restoNome.toLowerCase();
        }
        var formContext = context.getFormContext();
        var nomeDaConta = formContext.getAttribute("name").getValue();
        if (nomeDaConta != null) {
            var arrayNome = nomeDaConta.split(" ");
            for (var contador = 0; contador < arrayNome.length; contador++) {
                arrayNome[contador] = convertFirstCharacterToUppercase(arrayNome[contador]);
            }
            nomeDaConta = arrayNome.join(" ");
            formContext.getAttribute("name").setValue(nomeDaConta);
        } else {
            return;
        }
    },


    PorteOnChange: function (context) {
        var formContext = context.getFormContext();
        var campoPorte = "tc4_porte";
        var porte = formContext.getAttribute(campoPorte).getValue();
        var campoNivel = "tc4_niveldecliente";

        switch (porte) {
            case Tcc.Account.TC4_Porte.Pequeno:
                formContext.getAttribute(campoNivel).setValue(Tcc.Account.TC4_NivelDeCliente.Silver);
                break;
            case Tcc.Account.TC4_Porte.Medio:
                formContext.getAttribute(campoNivel).setValue(Tcc.Account.TC4_NivelDeCliente.Gold);
                break;
            case Tcc.Account.TC4_Porte.Grande:
                formContext.getAttribute(campoNivel).setValue(Tcc.Account.TC4_NivelDeCliente.Platinum);
                break;
        }



    },

    DynamicsCustomAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };

        var alertOptions = {
            height: 120,
            width: 200
        };

        Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
    }


}






    