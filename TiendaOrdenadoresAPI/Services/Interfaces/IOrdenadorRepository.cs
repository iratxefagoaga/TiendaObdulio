using TiendaOrdenadoresAPI.Models;

namespace TiendaOrdenadoresAPI.Services.Interfaces
{
    public interface IOrdenadorRepository
    {
        public List<Ordenador>? All();
        public Ordenador? GetById(int id);
        public void Add(Ordenador ordenador);
        public void Edit(Ordenador ordenador);
        public void Delete(int id);
    }
}
