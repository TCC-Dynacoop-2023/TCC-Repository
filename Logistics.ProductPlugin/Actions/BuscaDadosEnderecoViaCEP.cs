using Logistics.ProductPlugin.LogisticsCore;
using Logistics.ProductPlugin.VO;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using RestSharp;
using System.Activities;

namespace Logistics.ProductPlugin.Actions
{
    public class BuscaDadosEnderecoViaCEP : ActionCore
    {
        [Input("lgt_cep")]
        public InArgument<string> CEP { get; set; }

        [Output("lgt_logradouro")]
        public OutArgument<string> Logradouro { get; set; }
        [Output("lgt_complemento")]
        public OutArgument<string> Complemento { get; set; }
        [Output("lgt_bairro")]
        public OutArgument<string> Bairro { get; set; }
        [Output("lgt_localidade")]
        public OutArgument<string> Localidade { get; set; }
        [Output("lgt_uf")]
        public OutArgument<string> UF { get; set; }
        [Output("lgt_ibge")]
        public OutArgument<string> IBGE { get; set; }
        [Output("lgt_ddd")]
        public OutArgument<string> DDD { get; set; }

        public override void ExecuteAction(CodeActivityContext context)
        {
            var endereco = GetAddressByCEP(context);

            Logradouro.Set(context, endereco.Logradouro);
            Complemento.Set(context, endereco.Complemento);
            Bairro.Set(context, endereco.Bairro);
            Localidade.Set(context, endereco.Localidade);
            UF.Set(context, endereco.UF);
            IBGE.Set(context, endereco.IBGE);
            DDD.Set(context, endereco.DDD);
        }

        private EnderecoVO GetAddressByCEP(CodeActivityContext context)
        {
            var options = new RestClientOptions($"viacep.com.br/ws/{CEP.Get(context)}/json/")
            {
                MaxTimeout = -1
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);

            var enderecoVO = JsonConvert.DeserializeObject<EnderecoVO>(response.Content);

            return enderecoVO;
        }
    }
}
