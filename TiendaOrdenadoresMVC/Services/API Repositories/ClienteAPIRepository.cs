using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace MVC_ComponentesCodeFirst.Services.API_Repositories
{
    public class ClienteApiRepository : IGenericRepository<Cliente>
    {

        private readonly HttpClient _httpClient;
        private readonly string _url;

        public ClienteApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _url = MyConfig.GetValue<string>("AppSettings:urlApi");
        }
        public async Task Add(Cliente cliente)
        {
            StringContent content = new(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(_url + "/api/Clientes", content);

        }

        public async Task<List<Cliente>> All()
        {
            return await _httpClient.GetFromJsonAsync<List<Cliente>>(_url + "/api/Clientes") ?? throw new InvalidOperationException();
        }

        public async Task Delete(int id)
        {
            using var response = await _httpClient.DeleteAsync(_url + "/api/Clientes/" + id);
            await response.Content.ReadAsStringAsync();
        }

        public async Task DeleteRange(int[] input)
        {
            foreach (var id in input)
            {
                using var response = await _httpClient.DeleteAsync(_url + "/api/Clientes/" + id);
                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task Edit(Cliente cliente)
        {
            StringContent content = new(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(_url + "/api/Clientes/" + cliente.Id, content);

        }

        public async Task<Cliente?> GetById(int? id)
        {
            using var response = await _httpClient.GetAsync(_url + "/api/Clientes/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Cliente>(apiResponse);
        }
    }
}
