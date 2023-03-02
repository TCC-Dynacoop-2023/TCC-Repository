if (typeof (Logistics) == "undefined") { Logistics = {} }
if (typeof (Logistics.Account) == "undefined") { Logistics.Account = {} }

Logistics.Account = {
    OnCepChange: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var id = Xrm.Page.data.entity.getId();

        var cep = formContext.getAttribute("lgt_cep").getValue();

		var execute_lgt_BuscaDadosEnderecoViaCEP_Request = {
			// Parameters
			entity: { entityType: "account", id: id }, // entity
			CEP: cep, // Edm.String

			getMetadata: function () {
				return {
					boundParameter: "entity",
					parameterTypes: {
						entity: { typeName: "mscrm.account", structuralProperty: 5 },
						CEP: { typeName: "Edm.String", structuralProperty: 1 }
					},
					operationType: 0, operationName: "lgt_BuscaDadosEnderecoViaCEP"
				};
			}
		};

		Xrm.WebApi.online.execute(execute_lgt_BuscaDadosEnderecoViaCEP_Request).then(
			function success(response) {
				debugger;
				if (response.ok) { return response.json(); }
			}
		).then(function (responseBody) {
			debugger;
			var result = responseBody;
			console.log(result);
			// Return Type: mscrm.lgt_BuscaDadosEnderecoViaCEPResponse
			// Output Parameters
			var logradouro = result["Logradouro"]; // Edm.String
			formContext.getAttribute("lgt_logradouro").setValue(logradouro);

			var complemento = result["Complemento"]; // Edm.String
			formContext.getAttribute("lgt_complemento").setValue(complemento);

			var bairro = result["Bairro"]; // Edm.String
			formContext.getAttribute("lgt_bairro").setValue(bairro);

			var localidade = result["Localidade"]; // Edm.String
			formContext.getAttribute("lgt_localidade").setValue(localidade);

			var uf = result["UF"]; // Edm.String
			formContext.getAttribute("lgt_uf").setValue(uf);

			var ibge = result["IBGE"]; // Edm.String
			formContext.getAttribute("lgt_ibge").setValue(ibge);

			var ddd = result["DDD"]; // Edm.String
			formContext.getAttribute("lgt_ddd").setValue(ddd);

		}).catch(function (error) {
			debugger;
			console.log(error.message);
		});
    }
}