using TiendaOrdenadoresAPI.Models;

namespace TiendaOrdenadoresAPI.Services.Interfaces
{
    public interface IClienteRepository
    {
        public List<Cliente>? All();
        public Cliente? GetById(int? id);
        public void Add(Cliente cliente);
        public void Edit(Cliente cliente);
        public void Delete(int id);
    }
}
