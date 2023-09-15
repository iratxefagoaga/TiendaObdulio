namespace Ejercicio_ordenadores.Pedidos
{
    public class Factura : ICosteable
    {
        private readonly List<IColeccionable> _listaPedidos = new ();
        private decimal _precio;

        public void AñadirPedido(IColeccionable pedido)
        {
            _listaPedidos.Add(pedido);
            if (pedido is Pedido newPedido)
            {
                _precio += newPedido.GetPrecio();
            }
        }

        public decimal GetPrecio()
        {
            return _precio;
        }

        public bool HayEnStock(Almacen almacen)
        {
            foreach (var coleccionable in _listaPedidos)
            {
                var pedido = (Pedido?)coleccionable;
                if (pedido is { ListaOrdenadores: not null })
                    foreach (var ordenador1 in pedido.ListaOrdenadores)
                    {
                        var ordenador = (OrdenadorMejorado)ordenador1;
                        foreach (var componente in ordenador.ComponentesOrdenador())
                        {
                            if (almacen.ContainsKey(componente, out _))
                            {
                                almacen.QuitarComponente(componente);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
            }

            return true;
        }
    }
}
