using Logistics.Product.Environment1.Controll;
using Logistics.Product.Environment1.LogisticsISV;
using Logistics.Product.Environment1.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Logistics.Product.Environment1.Plugins
{
    public class OpportunityManager : PluginCore
    {
        
        public override void ExecutePlugin(IServiceProvider serviceProvider)

        {
            OpportunityController opportunityController = new OpportunityController(this.Service);
            Entity opportunity = (Entity)Context.InputParameters["Target"];

            string opportunityRegisterId = opportunityController.GeneratePattern();
            while(opportunityController.ComparePattern(opportunityRegisterId) != null)
            {
                opportunityRegisterId = opportunityController.GeneratePattern();
            }

            opportunity.Attributes["lgt_registroid_opportunity"] = opportunityRegisterId;
            Trace.WriteLine("adicionando padrao");
           

        }
    }
}
