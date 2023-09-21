using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IComponenteRepository : IGenericRepository<Componente>
    {
        public SelectList OrdenadoresLista(int componenteId = 0);
        public Task DeleteRange(int[] deleteInputs);
    }
}
