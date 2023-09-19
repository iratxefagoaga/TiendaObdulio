using System.Security.Policy;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Newtonsoft.Json;

namespace MVC_ComponentesCodeFirst.Services.API_Repositories
{
    public class ComponenteApiRepository :IComponenteRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public ComponenteApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _url = MyConfig.GetValue<string>("AppSettings:urlApi");
        }
        public async Task Add(Componente componente)
        {
            StringContent content = new(JsonConvert.SerializeObject(componente), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(_url+"/api/Componentes", content);

        }

        public async Task<List<Componente>> All()
        {
            return await _httpClient.GetFromJsonAsync<List<Componente>>(_url+"/api/Componentes") ?? throw new InvalidOperationException();
        }

        public async Task Delete(int id)
        {
            using var response = await _httpClient.DeleteAsync(_url+"/api/Componentes/" + id);
            await response.Content.ReadAsStringAsync();
        }

        public SelectList OrdenadoresLista(int ordenadorId = 0)
        {
            IOrdenadorRepository ordenadorRepository = new OrdenadorApiRepository(_httpClient);
            return ordenadorId == 0 ? new SelectList(ordenadorRepository.All().Result, "Id", "Descripcion") : new SelectList(ordenadorRepository.All().Result, "Id", "Descripcion", ordenadorId);
        }

        public async Task DeleteRange(int[] input)
        {
            foreach (var id in input)
            {
                using var response = await _httpClient.DeleteAsync(_url+"/api/Componentes/" + id);
                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task Edit(Componente componente)
        {
            StringContent content = new(JsonConvert.SerializeObject(componente), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(_url+"/api/Componentes/"+componente.Id, content);

        }

        public async Task<Componente?> GetById(int id)
        {
            using var response = await _httpClient.GetAsync(_url+"/api/Componentes/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Componente>(apiResponse);
        }
    }
}
