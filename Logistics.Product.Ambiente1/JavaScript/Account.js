if (typeof (Logistics) == "undefined") { Logistics = {} }
if (typeof (Logistics.Account) == "undefined") { Logistics.Account = {} }

Logistics.Account = {
    CNPJOnChange: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var cnpj = formContext.getAttribute("lgt_cnpj").getValue();
       
       
        if (cnpj != null) { 
            if (this.ValidaCNPJ(cnpj))
            {
                if (cnpj.length == 14) {
                    var formattedCNPJ = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
                    var id = Xrm.Page.data.entity.getId();

                    var queryAccountId = "";

                    if (id.length > 0) {
                        queryAccountId += " and accountid ne " + id;
                    }

                    Xrm.WebApi.online.retrieveMultipleRecords("account", "?$select=name&$filter=lgt_cnpj eq '" + formattedCNPJ + "'" + queryAccountId).then(
                        function success(results) {
                            if (results.entities.length == 0) {
                                formContext.getAttribute("lgt_cnpj").setValue(formattedCNPJ);
                            } else {
                                formContext.getAttribute("lgt_cnpj").setValue("");
                                Logistics.Account.DynamicsAlert("O CNPJ já existe no sistema", "CNPJ duplicado!")
                            }
                        },
                        function (error) {
                            Logistics.Account.DynamicsAlert("Por favor contato o administrador", "Erro do sistema")
                        }
                    );
                }
                else {
                    Logistics.Account.DynamicsAlert("O CNPJ digitado não é valido", "CNPJ inválido!")
                }
            }
            
        } else {
            Logistics.Account.DynamicsAlert("Informe um valor para o CNPJ", "CNPJ incorreto!")
        }
    },
    ValidaCNPJ: function (cnpj)
    {
        if (cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999") {
            formContext.getAttribute("lgt_cnpj").setValue(null);
            return false;
        }

        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0)) {
            formContext.getAttribute("lgt_cnpj").setValue(null);
            return false;
        }

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1)) {
            formContext.getAttribute("lgt_cnpj").setValue(null);
            return false;
        }

        return true;
    },
    DynamicsAlert: function (alertText, alertTitle) {
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
    },
    LetraMaiuscula: function (text)
    {
        var minhaFrase = text;
        var palavras = minhaFrase.split(" ");

        for (let i = 0; i < palavras.length; i++) {
            if (palavras[i] != "") {
                palavras[i] = palavras[i][0].toUpperCase() + palavras[i].substr(1);
            }
        }
        return palavras.join(" ");

    },
    NomeOnchange: function (executionContext)
    {
        var formContext = executionContext.getFormContext();

        var nomeConta = formContext.getAttribute("name").getValue();
        if (nomeConta != "" && nomeConta != null)
        {
            nomeConta = this.LetraMaiuscula(nomeConta);
            formContext.getAttribute("name").setValue(nomeConta);
        }
        else
            Logistics.Account.DynamicsAlert("Informe um nome valido", "Nome vazio!")
    }

}    