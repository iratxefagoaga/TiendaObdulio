using System.Security.Policy;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Newtonsoft.Json;

namespace MVC_ComponentesCodeFirst.Services.API_Repositories
{
    public class OrdenadorApiRepository :IOrdenadorRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public OrdenadorApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _url = MyConfig.GetValue<string>("AppSettings:urlApi");
        }
        public async Task Add(Ordenador ordenador)
        {
            StringContent content = new(JsonConvert.SerializeObject(ordenador), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(_url+"/api/Ordenadores", content);

        }

        public async Task<List<Ordenador>> All()
        {
            return await _httpClient.GetFromJsonAsync<List<Ordenador>>(_url+"/api/Ordenadores") ?? throw new InvalidOperationException();
        }

        public async Task Delete(int id)
        {
            using var response = await _httpClient.DeleteAsync(_url+"/api/Ordenadores/" + id);
            await response.Content.ReadAsStringAsync();
        }

        public async Task DeleteRange(int[] input)
        {
            foreach (var id in input)
            {
                using var response = await _httpClient.DeleteAsync(_url+"/api/Ordenadores/" + id);
                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task Edit(Ordenador ordenador)
        {
            StringContent content = new(JsonConvert.SerializeObject(ordenador), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(_url+"/api/Ordenadores/" + ordenador.Id, content);

        }

        public async Task<Ordenador?> GetById(int id)
        {
            using var response = await _httpClient.GetAsync(_url+"/api/Ordenadores/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Ordenador>(apiResponse);
        }

        public SelectList PedidosLista(Ordenador? ordenador = null)
        {
            IPedidoRepository pedidoRepository = new PedidoApiRepository(_httpClient);
            return ordenador == null ? new SelectList(pedidoRepository.All().Result, "Id", "Descripcion") : new SelectList(pedidoRepository.All().Result, "Id", "Descripcion", ordenador.PedidoId);
        }
    }
}
