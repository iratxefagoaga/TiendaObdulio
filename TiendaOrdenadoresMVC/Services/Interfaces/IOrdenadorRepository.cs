using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IOrdenadorRepository
    {
        public Task<List<Ordenador>> All();
        public Task<Ordenador?> GetById(int id);
        public Task Add(Ordenador ordenador);
        public Task Edit(Ordenador ordenador);
        public Task Delete(int id);
        public SelectList PedidosLista(Ordenador? ordenador = null);
        public Task DeleteRange(int[] deleteInputs);
    }
}
