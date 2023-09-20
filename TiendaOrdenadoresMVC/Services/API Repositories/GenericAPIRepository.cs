using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using MVC_ComponentesCodeFirst.Services.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace MVC_ComponentesCodeFirst.Services.API_Repositories
{
    public class GenericAPIRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly HttpClient _httpClient;
        private string _url;
        private string _urlComponentes;
        private string _urlOrdenadores;
        private string _urlPedidos;
        private string _urlFacturas;
        private string _urlClientes;
        public GenericAPIRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _url = ObtenerUrl(typeof(T));
            _urlComponentes = ObtenerUrl(typeof(Componente));
            _urlOrdenadores = ObtenerUrl(typeof(Ordenador));
            _urlPedidos = ObtenerUrl(typeof(Pedido));
            _urlFacturas = ObtenerUrl(typeof(Factura));
            _urlClientes = ObtenerUrl(typeof(Cliente));
        }

        private string ObtenerUrl(Type tipo)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var url = MyConfig.GetValue<string>("AppSettings:urlApi");
            if (tipo== typeof(Componente))
            {
                url += "/api/Componentes";
            }
            if (tipo== typeof(Ordenador))
            {
                url += "/api/Ordenadores";
            }
            if (tipo == typeof(Pedido))
            {
                url += "/api/Pedidos";
            }
            if (tipo == typeof(Factura))
            {
                url += "/api/Facturas";
            }
            if (tipo == typeof(Cliente))
            {
                url += "/api/Clientes";
            }

            return url;
        }

        public async Task Add(T obj)
        {
            StringContent content = new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(_url, content);
        }

        public async Task<List<T>> All()
        {
            return await _httpClient.GetFromJsonAsync<List<T>>(_url) ?? throw new InvalidOperationException();

        }

        public async Task Delete(int id)
        {
            using var response = await _httpClient.DeleteAsync(_url  + "/" + id);
            await response.Content.ReadAsStringAsync();
        }

        public async Task DeleteRange(int[] input)
        {
            foreach (var id in input)
            {
                using var response = await _httpClient.DeleteAsync(_url + "/" + id);
                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task Edit(T obj)
        {
            StringContent content = new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(_url + "/" + obj.Id, content);


        }

        public async Task<T?> GetById(int? id)
        {
            using var response = await _httpClient.GetAsync(_url + "/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(apiResponse);
        }

        public SelectList ListaClientesId(int id = 0)
        {
            var clientes = _httpClient.GetFromJsonAsync<List<Cliente>>(_urlClientes).Result;
            return id == 0 ? new SelectList(clientes, "Id", "Apellido") : new SelectList(clientes, "Id", "Apellido", id);

        }

        public SelectList ListaFacturasId(int id = 0)
        {
            var facturas = _httpClient.GetFromJsonAsync<List<Factura>>(_urlFacturas).Result;
            return id == 0 ? new SelectList(facturas, "Id", "Descripcion") : new SelectList(facturas, "Id", "Descripcion", id);
        }

        public SelectList OrdenadoresLista(int ordenadorId = 0)
        {
            var ordenadores = _httpClient.GetFromJsonAsync<List<Ordenador>>(_urlOrdenadores).Result;
            return ordenadorId == 0 ? new SelectList(ordenadores, "Id", "Descripcion") : new SelectList(ordenadores, "Id", "Descripcion", ordenadorId);

        }

        public SelectList PedidosLista(Ordenador? ordenador = null)
        {
            var pedidos =  _httpClient.GetFromJsonAsync<List<Pedido>>(_urlPedidos).Result;
            return ordenador == null ? new SelectList(pedidos, "Id", "Descripcion") : new SelectList(pedidos, "Id", "Descripcion", ordenador.PedidoId);

        }
    }
}
