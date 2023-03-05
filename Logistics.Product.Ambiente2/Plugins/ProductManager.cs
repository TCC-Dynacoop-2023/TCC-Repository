using Logistics.Product.Environment2.LogisticsISV;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Product.Environment2.Plugins
{
    public class ProductManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity productIntregration = (Entity)Context.InputParameters["Target"];
            bool validationProduct = (bool)productIntregration["dytcc_integrarproduto"];

            if (!validationProduct)
            {
                throw new InvalidPluginExecutionException("A criação de um produto deve ser realizada no Ambiente 1");
            }
        }
    }
}
