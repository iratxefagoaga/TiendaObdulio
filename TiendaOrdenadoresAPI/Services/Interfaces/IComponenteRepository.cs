using TiendaOrdenadoresAPI.Models;

namespace TiendaOrdenadoresAPI.Services.Interfaces
{
    public interface IComponenteRepository
    {
        public List<Componente>? All();
        public Componente? GetById(int id);
        public void Add(Componente componente);
        public void Edit(Componente componente);
        public void Delete(int id);
    }
}
