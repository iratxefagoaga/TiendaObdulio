using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace MVC_ComponentesCodeFirst.Services.API_Repositories
{
    public class FacturaApiRepository : IGenericRepository<Factura>
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public FacturaApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _url = MyConfig.GetValue<string>("AppSettings:urlApi");
        }
        public async  Task Add(Factura factura)
        {
            StringContent content = new(JsonConvert.SerializeObject(factura), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(_url+"/api/Facturas", content);

        }

        public async Task<List<Factura>> All()
        {
            return await _httpClient.GetFromJsonAsync<List<Factura>>(_url+"/api/Facturas") ?? throw new InvalidOperationException();
        }

        public async  Task Delete(int id)
        {
            using var response = await _httpClient.DeleteAsync(_url+"/api/Facturas/" + id);
            await response.Content.ReadAsStringAsync();
        }

        public async  Task DeleteRange(int[] input)
        {
            foreach (var id in input)
            {
                using var response = await _httpClient.DeleteAsync(_url+"/api/Facturas/" + id);
                await response.Content.ReadAsStringAsync();
            }
        }

        public async  Task Edit(Factura factura)
        {
            StringContent content = new(JsonConvert.SerializeObject(factura), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(_url+"/api/Facturas/" + factura.Id, content);

        }

        public async Task<Factura?> GetById(int? id)
        {
            using var response = await _httpClient.GetAsync(_url+"/api/Facturas/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Factura>(apiResponse);
        }
    }
}
