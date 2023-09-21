namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IGenericRepository<T> where T : IEntity 
    {
        public Task Add(T obj);
        public Task<List<T>> All();
        public Task Delete(int id);
        public Task DeleteRange(int[] input);
        public Task Edit(T obj);
        public Task<T?> GetById(int? id);
    }
}
