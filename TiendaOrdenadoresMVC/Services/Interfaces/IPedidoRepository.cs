using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IPedidoRepository
    {
        public Task<List<Pedido>> All();
        public Task<Pedido?> GetById(int? id);
        public SelectList ListaClientesId(int id = 0);
        public SelectList ListaFacturasId(int id = 0);
        public Task Add(Pedido pedido);
        public Task Edit(Pedido pedido);
        public Task Delete(int id);
        public Task DeleteRange(int[] deleteInputs);
    }
}
