if (typeof (Logistics) == "undefined") { Logistics = {} }
if (typeof (Logistics.Account) == "undefined") { Logistics.Account = {} }

Logistics.Account = {
    OnCepChange: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var productId = formContext.getAttribute("productnumber").getValue();


    }
}