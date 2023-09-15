using Ejercicio_ordenadores.Builders.Ordenadores;
using Ejercicio_ordenadores.Pedidos;

namespace Ejercicio_ordenadores.Builders.Pedidos
{
    public class PedidosBuilder : IPedidosBuilder
    {
        private IColeccionable? _pedido;
        public IColeccionable GetPedido(TipoPedido pedido)
        {
            _pedido = new Pedido();
            switch (pedido)
            {
                case TipoPedido.PedidoA:
                    {
                        _pedido.AñadirOrdenador(TipoOrdenadores.OrdenadorAndres);
                        _pedido.AñadirOrdenador(TipoOrdenadores.OrdenadorMaria);
                        break;
                    }
                case TipoPedido.PedidoB:
                    {
                        _pedido.AñadirOrdenador(TipoOrdenadores.OrdenadorAndresCf);
                        _pedido.AñadirOrdenador(TipoOrdenadores.OrdenadorTiburcioIi);
                        break;
                    }
            }
            return _pedido;
        }
    }
}
