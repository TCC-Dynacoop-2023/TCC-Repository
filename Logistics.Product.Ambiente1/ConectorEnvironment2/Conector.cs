using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Product.Environment1.ConectorEnvironment2
{
    internal class Conector
    {
        public static IOrganizationService organizationService()
        {
            string url = "org6595776b";
            string clientId = "86e401a6-df4d-4a4c-84e7-6deca55c3bbe";
            string clientSecret = "Fev8Q~sXJrHHGlUEWIqjatV~OzNIs_yu3l4XgaGs";

            CrmServiceClient serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{url}.crm2.dynamics.com/;AppId={clientId};ClientSecret={clientSecret};");

            if (!serviceClient.CurrentAccessToken.Equals(null))
                Console.WriteLine("Conexão Realizada com Sucesso");
            else
                Console.WriteLine("Erro na conexão");

            return serviceClient;

        }
    }
}
