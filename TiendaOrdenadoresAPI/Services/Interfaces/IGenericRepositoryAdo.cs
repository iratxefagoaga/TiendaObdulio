using TiendaOrdenadoresAPI.Models;

namespace TiendaOrdenadoresAPI.Services.Interfaces
{
    public interface IGenericRepositoryAdo<T> where T : class
    {
        public List<T>? All();
        public T? GetById(int id);
        public void Add(T obj);
        public void Edit(T obj);
        public void Delete(int id);
    }
}
