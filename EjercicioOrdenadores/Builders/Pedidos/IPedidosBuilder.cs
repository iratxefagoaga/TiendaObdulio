namespace Ejercicio_ordenadores.Builders.Pedidos
{
    public interface IPedidosBuilder
    {
        public IColeccionable GetPedido(TipoPedido pedido);
    }
}
