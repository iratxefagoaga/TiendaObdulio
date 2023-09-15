using Ejercicio_ordenadores.Pedidos;

namespace Ejercicio_ordenadores.Builders.Facturas
{
    public interface IFacturasBuilder
    {
        public Factura DameFactura(TipoFacturas tipoFactura, Almacen almacen);
    }
}
