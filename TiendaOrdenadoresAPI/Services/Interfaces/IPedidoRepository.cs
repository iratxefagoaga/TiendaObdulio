using TiendaOrdenadoresAPI.Models;

namespace TiendaOrdenadoresAPI.Services.Interfaces
{
    public interface IPedidoRepository
    {
        public List<Pedido>? All();
        public Pedido? GetById(int? id);
        public void Add(Pedido pedido);
        public void Edit(Pedido pedido);
        public void Delete(int id);
    }
}
