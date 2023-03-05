using Logistics.Product.Environment1.ConectorEnvironment2;
using Logistics.Product.Environment1.LogisticsISV;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Product.Environment1.Plugins
{
    public class ProductManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity product = (Entity)Context.InputParameters["Target"];
            product["dytcc_integrarproduto"] = true;
            IOrganizationService serviceCliente = Conector.organizationService();
            serviceCliente.Create(product);

        }
    }
}
