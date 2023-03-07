using Logistics.Product.Environment1.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Product.Environment1.Controll
{
    public class OpportunityController
    {
        public Opportunity opportunity { get; set; }
        public IOrganizationService organizationService { get; set; }

        public OpportunityController(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
            this.opportunity = new Opportunity(organizationService);
        }

        //public string SetPattern()
        //{
        //   return opportunity.SetPattern();
        //}
        public string GeneratePattern()
        {
            return opportunity.GeneratePattern();
        }
        public Entity ComparePattern(string pattern)
        {
            return opportunity.ComparePattern(pattern);
        }
    }
}
