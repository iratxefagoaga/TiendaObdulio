using Ejercicio_ordenadores.Builders.Ordenadores;
using Ejercicio_ordenadores.Builders.Pedidos;
using Ejercicio_ordenadores.Interfaces;
using Ejercicio_ordenadores.Pedidos;

namespace EjercicioOrdenadores.Test
{
    [TestClass]
    public class OrdenadoresTestC
    {
        private readonly IPedidosBuilder _builderPedidos = new PedidosBuilder();
        private readonly IOrdenadorBuilder _ordenadorBuilder = new OrdenadorBuilderMejorado();

        [TestMethod]
        public void CrearPedidoAMano()
        {
            IColeccionable pedido = new Pedido();
            pedido.AñadirOrdenador(TipoOrdenadores.OrdenadorMaria);
            Assert.IsNotNull(pedido);
            Assert.IsTrue((pedido as Pedido)?.ListaOrdenadores?.Contains(_ordenadorBuilder.DameOrdenador(TipoOrdenadores.OrdenadorMaria)));
        }
        [TestMethod]
        public void CrearPedidoA()
        {
            var pedido = _builderPedidos.GetPedido(TipoPedido.PedidoA);
            Assert.IsNotNull(pedido);
            var ordenadorMaria = _ordenadorBuilder.DameOrdenador(TipoOrdenadores.OrdenadorMaria);
            var ordenadorAndres = _ordenadorBuilder.DameOrdenador(TipoOrdenadores.OrdenadorAndres);
            var listaOrdenadores = (pedido as Pedido)?.ListaOrdenadores;
            Assert.IsTrue(listaOrdenadores?.Contains(ordenadorMaria));
            Assert.IsTrue(listaOrdenadores?.Contains(ordenadorAndres));
        }
        [TestMethod]
        public void CrearPedidoB()
        {
            var pedido = _builderPedidos.GetPedido(TipoPedido.PedidoB);
            Assert.IsNotNull(pedido);
            var ordenadorTiburcio = _ordenadorBuilder.DameOrdenador(TipoOrdenadores.OrdenadorTiburcioIi);
            var ordenadorAndresCf = _ordenadorBuilder.DameOrdenador(TipoOrdenadores.OrdenadorAndresCf);
            var listaOrdenadores = (pedido as Pedido)?.ListaOrdenadores;
            Assert.IsTrue(listaOrdenadores?.Contains(ordenadorAndresCf));
            Assert.IsTrue(listaOrdenadores?.Contains(ordenadorTiburcio));
        }

        [TestMethod]
        public void QuitarOrdenadoresPedido()
        {
            var pedido = _builderPedidos.GetPedido(TipoPedido.PedidoA);
            Assert.IsNotNull(pedido);
            pedido.AñadirOrdenador(TipoOrdenadores.OrdenadorAndresCf);
            Assert.IsTrue((pedido as Pedido)?.ListaOrdenadores?.Count == 3);
            pedido.QuitarOrdenador(TipoOrdenadores.OrdenadorAndres);
            Assert.IsTrue(!(pedido as Pedido)?.ListaOrdenadores?.Contains(_ordenadorBuilder.DameOrdenador(TipoOrdenadores.OrdenadorAndres)));
        }

        [TestMethod]
        public void ObtenerPrecioPedido()
        {
            var pedidoA = _builderPedidos.GetPedido(TipoPedido.PedidoA);
            Assert.IsNotNull(pedidoA);
            var precioPedidoA = (pedidoA as Pedido)?.GetPrecio();
            Assert.AreEqual(840, precioPedidoA);
            var pedidoB = _builderPedidos.GetPedido(TipoPedido.PedidoB);
            Assert.IsNotNull(pedidoB);
            var precioPedidoB = (pedidoB as Pedido)?.GetPrecio();
            Assert.AreEqual(1048, precioPedidoB);
        }
        [TestMethod]
        public void ObtenerTemperaturaPedido()
        {
            var pedidoA = _builderPedidos.GetPedido(TipoPedido.PedidoA);
            Assert.IsNotNull(pedidoA);
            var temperaturaPedidoA = (pedidoA as Pedido)?.GetTemperatura();
            Assert.AreEqual(153, temperaturaPedidoA);
            var pedidoB = _builderPedidos.GetPedido(TipoPedido.PedidoB);
            Assert.IsNotNull(pedidoB);
            var temperaturaPedidoB = (pedidoB as Pedido)?.GetTemperatura();
            Assert.AreEqual(233, temperaturaPedidoB);
        }
    }
}
