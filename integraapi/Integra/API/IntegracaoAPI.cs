
using Integra.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Configuration;

namespace Integra.API
{
    public class IntegracaoAPI
    {
        private string _apikey = ConfigurationManager.AppSettings["apikey"];
        private string _apiToken = ConfigurationManager.AppSettings["apiToken"];
        private string _urlAPI = ConfigurationManager.AppSettings["api_vtex"];
        private string _urlAPILoja = ConfigurationManager.AppSettings["api_vtex_loja"];

        public Categoria RetornaCategoria(string idCategoria)
        {
            Categoria categoria = new Categoria();
            RestClient client = new RestClient(this._urlAPILoja);
            RestRequest request = new RestRequest("api/catalog_system/pvt/category/" + idCategoria, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-vtex-api-appKey", this._apikey);
            request.AddHeader("x-vtex-api-appToken", this._apiToken);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                categoria = JsonConvert.DeserializeObject<Categoria>(response.Content);
            }
            return categoria;
        }

        public List<cliente> RetornaCliente(out string token)
        {
            List<cliente> list = new List<cliente>();
            string str = string.Empty;
            RestClient client = new RestClient(this._urlAPI);
            RestRequest request = new RestRequest("/dataentities/CL/scroll?isCluster=true&_size=100&_where=document is not null&_fields=_all", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-vtex-api-appKey", this._apikey);
            request.AddHeader("x-vtex-api-appToken", this._apiToken);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                list = JsonConvert.DeserializeObject<List<cliente>>(response.Content);
                string str2 = Convert.ToString(response.Headers[8].Value);
                if (!string.IsNullOrEmpty(str2))
                {
                    str = Convert.ToString(str2);
                }
            }
            token = str;
            return list;
        }

        public ClienteEndereco RetornaClienteEndereco(string idCliente)
        {
            List<ClienteEndereco> source = new List<ClienteEndereco>();
            RestClient client = new RestClient(this._urlAPI);
            RestRequest request = new RestRequest("/dataentities/AD/search?_where=id=" + idCliente + "&_fields=_all", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-vtex-api-appKey", this._apikey);
            request.AddHeader("x-vtex-api-appToken", this._apiToken);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                source = JsonConvert.DeserializeObject<List<ClienteEndereco>>(response.Content);
            }
            return ((source != null) ? source.FirstOrDefault<ClienteEndereco>() : null);
        }

        public List<cliente> RetornaClienteProximoBloco(string token)
        {
            List<cliente> list = new List<cliente>();
            RestClient client = new RestClient(this._urlAPI);
            RestRequest request = new RestRequest("dataentities/CL/scroll?_token=" + token, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-vtex-api-appKey", this._apikey);
            request.AddHeader("x-vtex-api-appToken", this._apiToken);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                list = JsonConvert.DeserializeObject<List<cliente>>(response.Content);
            }
            return list;
        }

        public Pedido RetornaListaPedido(string cpf)
        {
            Pedido pedido = new Pedido();
            RestClient client = new RestClient(this._urlAPILoja);
            RestRequest request = new RestRequest("/api/oms/pvt/orders?q=" + cpf + "&f_status=invoiced", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-vtex-api-appKey", this._apikey);
            request.AddHeader("x-vtex-api-appToken", this._apiToken);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                pedido = JsonConvert.DeserializeObject<Pedido>(response.Content);
            }
            return pedido;
        }

        public PedidoCompleto RetornaListaPedidoPorNumero(string numero)
        {
            PedidoCompleto completo = new PedidoCompleto();
            RestClient client = new RestClient(this._urlAPILoja);
            RestRequest request = new RestRequest("/api/oms/pvt/orders/" + numero, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-vtex-api-appKey", this._apikey);
            request.AddHeader("x-vtex-api-appToken", this._apiToken);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                completo = JsonConvert.DeserializeObject<PedidoCompleto>(response.Content);
            }
            return completo;
        }

        public Produto RetornaProduto(string idProduto)
        {
            Produto produto = new Produto();
            RestClient client = new RestClient(this._urlAPILoja);
            RestRequest request = new RestRequest("/api/catalog_system/pvt/products/ProductGet/" + idProduto, Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-vtex-api-appKey", this._apikey);
            request.AddHeader("x-vtex-api-appToken", this._apiToken);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                produto = JsonConvert.DeserializeObject<Produto>(response.Content);
            }
            return produto;
        }
    }
}

