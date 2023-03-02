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
        [Input("CEP")]
        public InArgument<string> CEP { get; set; }

        [Output("Logradouro")]
        public OutArgument<string> Logradouro { get; set; }
        [Output("Complemento")]
        public OutArgument<string> Complemento { get; set; }
        [Output("Bairro")]
        public OutArgument<string> Bairro { get; set; }
        [Output("Localidade")]
        public OutArgument<string> Localidade { get; set; }
        [Output("UF")]
        public OutArgument<string> UF { get; set; }
        [Output("IBGE")]
        public OutArgument<string> IBGE { get; set; }
        [Output("DDD")]
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
            try
            {
                var options = new RestClientOptions("https://viacep.com.br/ws/")
                {
                    MaxTimeout = -1
                };
                var client = new RestClient(options);

                var args = new
                {
                    cep = CEP.Get(context),
                };
                return client.GetJson<EnderecoVO>("{cep}/json", args);
            }
            catch
            {
                throw new System.Exception("Erro ao consumir a api");
            }
        }
    }
}
