using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IComponenteRepository
    {
        public Task<List<Componente>> All();
        public Task<Componente?> GetById(int id);
        public Task Add(Componente componente);
        public Task Edit(Componente componente);
        public Task Delete(int id);
        public SelectList OrdenadoresLista(int componenteId = 0);
        public Task DeleteRange(int[] deleteInputs);
    }
}
