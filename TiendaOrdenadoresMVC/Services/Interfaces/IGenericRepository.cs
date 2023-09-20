using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IGenericRepository<T> where T : IEntity 
    {
        public Task<List<T>> All();
        public Task<T?> GetById(int? id);
        public Task Add(T obj);
        public Task Edit(T obj);
        public Task Delete(int id);
        public Task DeleteRange(int[] input);
        public SelectList OrdenadoresLista(int ordenadorId = 0);

        public SelectList PedidosLista(Ordenador? ordenador = null);
        public SelectList ListaClientesId(int id = 0);

        public SelectList ListaFacturasId(int id = 0);
    }
}
