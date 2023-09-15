using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace MVC_ComponentesCodeFirst.Services.API_Repositories
{
    public class ClienteApiRepository : IClienteRepository
    {

        private readonly HttpClient _httpClient;

        public ClienteApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async  Task Add(Cliente cliente)
        {
                StringContent content = new(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                await _httpClient.PostAsync("https://localhost:7135/api/Clientes", content);
                
        }

        public async Task<List<Cliente>> All()
        {
            return await _httpClient.GetFromJsonAsync<List<Cliente>>("https://localhost:7135/api/Clientes") ?? throw new InvalidOperationException();
        }

        public async  Task Delete(int id)
        {
            using var response = await _httpClient.DeleteAsync("https://localhost:7135/api/Clientes/" + id);
            await response.Content.ReadAsStringAsync();
        }

        public async  Task DeleteRange(int[] input)
        {
            foreach (var id in input)
            {
                using var response = await _httpClient.DeleteAsync("https://localhost:7135/api/Clientes/" + id);
                await response.Content.ReadAsStringAsync();
            }
        }

        public async  Task Edit(Cliente cliente)
        {
            StringContent content = new(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("https://localhost:7135/api/Clientes/" + cliente.Id, content);

        }

        public async Task<Cliente?> GetById(int? id)
        {
            using var response = await _httpClient.GetAsync("https://localhost:7135/api/Clientes/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Cliente>(apiResponse);
        }
    }
}
