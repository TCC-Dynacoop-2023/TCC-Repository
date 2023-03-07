using Logistics.Product.Environment2.LogisticsISV;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Product.SharedProject1.Model
{
    public class Opportunity
    {
        public IOrganizationService serviceCliente { get; set; }

        public Opportunity(IOrganizationService serviceClient) 
        { 

        }
    }
}
