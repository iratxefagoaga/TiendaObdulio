using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IClienteRepository
    {
        public Task<List<Cliente>> All();
        public Task<Cliente?> GetById(int? id);
        public Task Add(Cliente cliente);
        public Task Edit(Cliente cliente);
        public Task Delete(int id);
        public Task DeleteRange(int[] input);
    }
}
