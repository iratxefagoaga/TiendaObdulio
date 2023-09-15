using Ejercicio_ordenadores.Builders.Componentes;
using Ejercicio_ordenadores.Builders.Ordenadores;
using Ejercicio_ordenadores.Interfaces;
using Ejercicio_ordenadores.Ordenadores;

namespace EjercicioOrdenadores.Test
{
    [TestClass]
    public class OrdenadoresTestB
    {
        private readonly IOrdenadorBuilder _constructorOrdenador = new OrdenadorBuilderMejorado();
        private readonly IComponenteBuilder _componenteBuilder = new ComponenteBuilder();
        [TestMethod]
        public void CrearOrdenadoresMejorados()
        {
            var ordenador0 = _constructorOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorMaria);
            Assert.IsNotNull(ordenador0);
            var ordenador1 = _constructorOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorAndres);
            Assert.IsNotNull(ordenador1);
            var ordenador2 = _constructorOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorAndresCf);
            Assert.IsNotNull(ordenador2);
            var ordenador3 = _constructorOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorTiburcioIi);
            Assert.IsNotNull(ordenador3);
        }
        [TestMethod]
        public void ObtenerPrecioTemperaturaOrdenadoresMejorados()
        {
            var ordenador = _constructorOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorAndresCf);
            Assert.IsNotNull(ordenador);
            Assert.AreEqual(593,ordenador.GetPrecio());
            Assert.AreEqual(158,ordenador.GetTemperatura());
            var ordenador2 = _constructorOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorTiburcioIi);
            Assert.IsNotNull(ordenador2);
            Assert.AreEqual(455,ordenador2.GetPrecio());
            Assert.AreEqual(75, ordenador2.GetTemperatura());
        }

        [TestMethod]
        public void AñadirDiscosOrdenador()
        {
            var ordenador = _constructorOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorAndres);
            Assert.IsNotNull(ordenador);
            
            (ordenador as OrdenadorMejorado)?.AñadirDiscosSecundarios(_componenteBuilder.GetDisco(Discos.Disco788Fg));
            var discosSecundarios = (ordenador as OrdenadorMejorado)?.DiscosSecundarios;
            List<IComponente?> listaEsperada = new ()
                { _componenteBuilder.GetDisco(Discos.Disco788Fg) };
            var i = 0;
            if (discosSecundarios != null)
                foreach (var disco in discosSecundarios)
                {
                    Assert.AreEqual((object?)listaEsperada[i], disco);
                    i++;
                }
        }
        [TestMethod]
        public void EliminarDiscosOrdenador()
        {
            var ordenador = _constructorOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorAndres);
            Assert.IsNotNull(ordenador);

            (ordenador as OrdenadorMejorado)?.AñadirDiscosSecundarios(_componenteBuilder.GetDisco(Discos.Disco788Fg));
            (ordenador as OrdenadorMejorado)?.AñadirDiscosSecundarios(_componenteBuilder.GetDisco(Discos.Disco788Fg));
            (ordenador as OrdenadorMejorado)?.QuitarDiscoSecundario(_componenteBuilder.GetDisco(Discos.Disco788Fg));

            var discosSecundarios = (ordenador as OrdenadorMejorado)?.DiscosSecundarios;
            if (discosSecundarios != null)
            {
                Assert.AreEqual(1, discosSecundarios.Count);
                List<IComponente?> listaEsperada = new ()
                    { _componenteBuilder.GetDisco(Discos.Disco788Fg) };
                var i = 0;
                foreach (var disco in discosSecundarios)
                {
                    Assert.AreEqual(listaEsperada[i], disco);
                    i++;
                }
            }
        }
    }
}
