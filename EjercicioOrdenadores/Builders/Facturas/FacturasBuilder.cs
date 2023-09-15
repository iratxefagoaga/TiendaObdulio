using Ejercicio_ordenadores.Pedidos;
using Ejercicio_ordenadores.Builders.Pedidos;

namespace Ejercicio_ordenadores.Builders.Facturas
{
    public class FacturasBuilder : IFacturasBuilder
    {
        private readonly IPedidosBuilder _pedidoBuilder = new PedidosBuilder();

        public Factura DameFactura(TipoFacturas tipoFactura, Almacen almacen)
        {
            Factura factura = new ();
            switch (tipoFactura)
            {
                case TipoFacturas.FacturaA:
                {
                    factura.AñadirPedido(_pedidoBuilder.GetPedido(TipoPedido.PedidoA));
                    break;
                }
                case TipoFacturas.FacturaB:
                {
                    factura.AñadirPedido(_pedidoBuilder.GetPedido(TipoPedido.PedidoB));
                    break;
                }
                case TipoFacturas.FacturaC:
                {
                    factura.AñadirPedido(_pedidoBuilder.GetPedido(TipoPedido.PedidoA));
                    factura.AñadirPedido(_pedidoBuilder.GetPedido(TipoPedido.PedidoB));
                    break;
                }
                default: return factura;
            }

            if (factura.HayEnStock(almacen))
                return factura;
            throw new Exception("NO HAY STOCK");
        }

    }
}
