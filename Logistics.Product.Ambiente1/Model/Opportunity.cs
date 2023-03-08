using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Product.Environment1.Model
{
    public class Opportunity
    {
        public IOrganizationService organizationService { get; set; }
        public string logicalName { get; set; }
       

        public Opportunity(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
            this.logicalName = "opportunity";
        }

        public string GeneratePattern()
        {

            Random random = new Random();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


            // Gerando sequencia de 5 numeros inteiros
            int[] number = new int[3];
            number[0] = random.Next(10000, 100000);
            // Gerando um numeros inteiro
            number[1] = random.Next(1, 10);
            number[2] = random.Next(1, 10);

            // Gera a primeira letra aleatória
            char letter1 = letters[random.Next(letters.Length)];
            char letter2 = letters[random.Next(letters.Length)];

            // Retorna a string no padrão OPP-12365-A1A2
            return string.Format($"OPP-" + number[0] + "-" + letter1 + number[1] + letter2 + number[2]);
        }

        public Entity ComparePattern(string pattern)
        {
            QueryExpression queryPattern = new QueryExpression(this.logicalName);
            queryPattern.Criteria.AddCondition("lgt_registroid_opportunity", ConditionOperator.Equal, pattern);
            EntityCollection entityCollection = this.organizationService.RetrieveMultiple(queryPattern);

            if (entityCollection.Entities.Count() > 0)
                return entityCollection.Entities.FirstOrDefault();
            else
                return null;
        }

    }
}
