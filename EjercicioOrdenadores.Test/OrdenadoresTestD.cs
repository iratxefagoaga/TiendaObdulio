using Ejercicio_ordenadores.Builders.Componentes;
using Ejercicio_ordenadores.Builders.Facturas;
using Ejercicio_ordenadores.Interfaces;
using Ejercicio_ordenadores.Pedidos;

namespace EjercicioOrdenadores.Test
{
    [TestClass]
    public class OrdenadoresTestD
    {
        private readonly IComponenteBuilder _componenteBuilder = new ComponenteBuilder();
        private readonly Almacen _almacen = new ();
        [TestMethod]
        public void CrearAlmacen()
        {
            int[] numerosStockProcesadores = { 1, 1, 2, 1, 2, 1 };
            int[] numerosStockMemorias = { 2, 1, 1 };
            int[] numerosstockDiscos = { 1, 2, 1, 1, 1, 1, 1, 1, 0 };

            int i = 0;
            foreach (Procesadores procesador in Enum.GetValues(typeof(Procesadores)))
            {
                if (procesador != Procesadores.None)
                {
                    IComponente? componente = _componenteBuilder.GetProcesador(procesador);
                    _almacen.AñadirComponente(componente, numerosStockProcesadores[i]);
                    i++;
                    Assert.IsTrue(componente != null && _almacen.StockComponentes.ContainsKey(componente));
                    
                }
            }
            i = 0;
            foreach (Memorias memoria in Enum.GetValues(typeof(Memorias)))
            {
                if (memoria != Memorias.None)
                {
                    IComponente? componente = _componenteBuilder.GetMemoria(memoria);
                    _almacen.AñadirComponente(componente, numerosStockMemorias[i]);
                    i++;
                    Assert.IsTrue(componente != null && _almacen.StockComponentes.ContainsKey(componente));
                    
                }
            }
            i = 0;
            foreach (Discos disco in Enum.GetValues(typeof(Discos)))
            {
                if(disco != Discos.None)
                {
                    IComponente? componente = _componenteBuilder.GetDisco(disco);
                    _almacen.AñadirComponente(componente, numerosstockDiscos[i]);

                    i++;
                    Assert.IsTrue(componente != null && _almacen.StockComponentes.ContainsKey(componente));
           
                }
            }
            Dictionary<IComponente, int> listaComponentes = _almacen.StockComponentes;
            Assert.AreEqual(18, listaComponentes.Count);
        }

        [TestMethod]
        public void QuitarComponentesAlmacen()
        {
            CrearAlmacen();
            _almacen.QuitarComponente(_componenteBuilder.GetDisco(Discos.Disco1789Xcd));
            Assert.AreEqual(17, _almacen.StockComponentes.Count);
        }
        [TestMethod]
        public void CrearFacturas()
        {
            CrearAlmacen();
            IFacturasBuilder facturasBuilder = new FacturasBuilder();
            Factura facturaA = facturasBuilder.DameFactura(TipoFacturas.FacturaA, _almacen);
            Assert.IsNotNull(facturaA);
            Assert.AreEqual(840, facturaA.GetPrecio());

            try
            {
                facturasBuilder.DameFactura(TipoFacturas.FacturaB, _almacen);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("NO HAY STOCK", ex.Message);
            }

            try
            {
                facturasBuilder.DameFactura(TipoFacturas.FacturaC, _almacen);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("NO HAY STOCK", ex.Message);

            }
        }
    }
}
