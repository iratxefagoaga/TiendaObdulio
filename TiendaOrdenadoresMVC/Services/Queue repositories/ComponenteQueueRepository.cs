//using Azure.Storage.Queues;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using MVC_ComponentesCodeFirst.Models;
//using MVC_ComponentesCodeFirst.Services.Interfaces;
//using Newtonsoft.Json;
//using System.Threading.Tasks;
//using Azure.Storage.Queues;
//using Azure.Storage.Queues.Models;
//using System.Security.Policy;
//using System.Net.Http;

//namespace MVC_ComponentesCodeFirst.Services.Queue_repositories
//{
//    public class ComponenteQueueRepository : IComponenteRepository
//    {
//        private QueueClient _queue;
//        private string _url;

//        public ComponenteQueueRepository()
//        {
//            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
//            var connectionString = MyConfig.GetValue<string>("AppSettings:storageConnectionString");
//            _queue = new QueueClient(connectionString, "mystoragequeue");
//            _url = MyConfig.GetValue<string>("AppSettings:urlApi");
//        }
//        public async Task Add(Componente componente)
//        {
//            _httpClient.PostAsync(_url + "/api/Componentes", content)
//           string message = _url + "/api/Componentes"

//        }

//        public Task<List<Componente>> All()
//        {
//            throw new NotImplementedException();
//        }

//        public Task Delete(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task DeleteRange(int[] deleteInputs)
//        {
//            throw new NotImplementedException();
//        }

//        public Task Edit(Componente componente)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Componente?> GetById(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public SelectList OrdenadoresLista(int componenteId = 0)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
