using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IOrdenadorRepository : IGenericRepository<Ordenador>
    {
        public SelectList PedidosLista(Ordenador? ordenador = null);
    }
}
