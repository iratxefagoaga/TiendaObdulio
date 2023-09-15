using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Newtonsoft.Json;

namespace MVC_ComponentesCodeFirst.Services.API_Repositories
{
    public class PedidoApiRepository :IPedidoRepository
    {
        private readonly HttpClient _httpClient;

        public PedidoApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async  Task Add(Pedido pedido)
        {
            StringContent content = new(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("https://localhost:7135/api/Pedidos", content);

        }

        public async Task<List<Pedido>> All()
        {
            return await _httpClient.GetFromJsonAsync<List<Pedido>>("https://localhost:7135/api/Pedidos") ?? throw new InvalidOperationException();
        }

        public async  Task Delete(int id)
        {
            using var response = await _httpClient.DeleteAsync("https://localhost:7135/api/Pedidos/" + id);
            await response.Content.ReadAsStringAsync();
        }

        public async  Task DeleteRange(int[] input)
        {
            foreach (var id in input)
            {
                using var response = await _httpClient.DeleteAsync("https://localhost:7135/api/Pedidos/" + id);
                await response.Content.ReadAsStringAsync();
            }
        }

        public async  Task Edit(Pedido pedido)
        {
            StringContent content = new(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("https://localhost:7135/api/Pedidos/" + pedido.Id, content);

        }

        public async Task<Pedido?> GetById(int? id)
        {
            using var response = await _httpClient.GetAsync("https://localhost:7135/api/Pedidos/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Pedido>(apiResponse);
        }

        public SelectList ListaClientesId(int id = 0)
        {
            IClienteRepository repositorioCliente = new ClienteApiRepository(_httpClient);
            return id == 0 ? new SelectList(repositorioCliente.All().Result, "Id", "Apellido") : new SelectList(repositorioCliente.All().Result, "Id", "Apellido", id);

        }

        public SelectList ListaFacturasId(int id = 0)
        {
            IFacturasRepository facturasRepository = new FacturaApiRepository(_httpClient);
            return id == 0 ? new SelectList(facturasRepository.All().Result, "Id", "Descripcion") : new SelectList(facturasRepository.All().Result, "Id", "Descripcion", id);

        }
    }
}
