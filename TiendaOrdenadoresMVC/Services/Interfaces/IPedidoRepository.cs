using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        public SelectList ListaClientesId(int id = 0);
        public SelectList ListaFacturasId(int id = 0);
    }
}
