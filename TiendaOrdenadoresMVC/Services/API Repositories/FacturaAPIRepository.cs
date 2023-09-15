using System.Text;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Newtonsoft.Json;

namespace MVC_ComponentesCodeFirst.Services.API_Repositories
{
    public class FacturaApiRepository :IFacturasRepository
    {
        private readonly HttpClient _httpClient;

        public FacturaApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async  Task Add(Factura factura)
        {
            StringContent content = new(JsonConvert.SerializeObject(factura), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("https://localhost:7135/api/Facturas", content);

        }

        public async Task<List<Factura>> All()
        {
            return await _httpClient.GetFromJsonAsync<List<Factura>>("https://localhost:7135/api/Facturas") ?? throw new InvalidOperationException();
        }

        public async  Task Delete(int id)
        {
            using var response = await _httpClient.DeleteAsync("https://localhost:7135/api/Facturas/" + id);
            await response.Content.ReadAsStringAsync();
        }

        public async  Task DeleteRange(int[] input)
        {
            foreach (var id in input)
            {
                using var response = await _httpClient.DeleteAsync("https://localhost:7135/api/Facturas/" + id);
                await response.Content.ReadAsStringAsync();
            }
        }

        public async  Task Edit(Factura factura)
        {
            StringContent content = new(JsonConvert.SerializeObject(factura), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("https://localhost:7135/api/Facturas/" + factura.Id, content);

        }

        public async Task<Factura?> GetById(int? id)
        {
            using var response = await _httpClient.GetAsync("https://localhost:7135/api/Facturas/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Factura>(apiResponse);
        }
    }
}
