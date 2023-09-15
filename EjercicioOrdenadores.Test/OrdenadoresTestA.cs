using System.ComponentModel.DataAnnotations;
using Ejercicio_ordenadores.Builders.Componentes;
using Ejercicio_ordenadores.Builders.Ordenadores;
using Ejercicio_ordenadores.ComponentesOrdenador;
using Ejercicio_ordenadores.Interfaces;

namespace EjercicioOrdenadores.Test
{
    [TestClass]
    public class OrdenadoresTestA
    {
        private readonly IOrdenadorBuilder _builderOrdenador = new OrdenadorBuilder();

        [TestMethod]
        public void CrearDiscoBien()
        {
            IComponente disco = new Disco("Disco1",20, 30, 30,"XFLG");
            Assert.IsNotNull(disco);
            Assert.AreEqual(20, disco.Megas);
            Assert.AreEqual(30, disco.GetPrecio());
            Assert.AreEqual(30, disco.GetTemperatura());
            Assert.AreEqual("Disco1", disco.Descripcion);
            Assert.AreEqual("XFLG", disco.NumeroSerie);
        }

        [TestMethod]
        public void CrearDiscoMal()
        {
            IComponente disco = new Disco("",-20, -30, -30,"");
            Assert.IsNotNull(disco);
            ValidationAttribute validador = new ValidadorComponentesAttribute();
            Assert.AreEqual(false, validador.IsValid(disco));
        }

        [TestMethod]
        public void CrearMemoriaBien()
        {
            IComponente memoria = new Memoria("Memoria1", 20, 30, 30, "XFLG");
            Assert.IsNotNull(memoria);
            Assert.AreEqual(20, memoria.Megas);
            Assert.AreEqual(30, memoria.GetPrecio());
            Assert.AreEqual(30, memoria.GetTemperatura());
            Assert.AreEqual("Memoria1", memoria.Descripcion);
            Assert.AreEqual("XFLG", memoria.NumeroSerie);
        }

        [TestMethod]
        public void CrearMemoriaMal()
        {
            IComponente memoria = new Memoria("",-20, -30, -30,"");
            Assert.IsNotNull(memoria);
            ValidationAttribute validador = new ValidadorComponentesAttribute();
            Assert.AreEqual(false, validador.IsValid(memoria));
        }

        [TestMethod]
        public void CrearProcesadorBien()
        {
            IComponente procesador = new Procesador("Procesador1", 20, 30, 30, "XFLG");
            Assert.IsNotNull(procesador);
            Assert.AreEqual(20, procesador.Cores);
            Assert.AreEqual(30, procesador.GetPrecio());
            Assert.AreEqual(30, procesador.GetTemperatura());
        }

        [TestMethod]
        public void CrearProcesadorMal()
        {
            IComponente procesador = new Procesador("", -4, -30, -30, "");
            Assert.IsNotNull(procesador);
            ValidationAttribute validador = new ValidadorComponentesAttribute();
            Assert.AreEqual(false, validador.IsValid(procesador));
        }

        [TestMethod]
        public void ClonarMemorias()
        {
            IComponente memoria = new Memoria("Memoria1", 20, 30, 30, "XFLG");
            var nuevaMemoria = (memoria as IClonable)?.Clone();
            var memoria1 = nuevaMemoria as Memoria;
            Assert.AreEqual(memoria.Megas, memoria1?.GetMemoria());
            Assert.AreEqual(memoria.GetPrecio(), memoria1?.GetPrecio());
            Assert.AreEqual(memoria.GetTemperatura(), memoria1?.GetTemperatura());

        }

        [TestMethod]
        public void ModificarClonMemoria()
        {
            IComponente memoria = new Memoria("Memoria1", 20, 30, 30, "XFLG");
            var nuevaMemoria = (memoria as IClonable)?.Clone();
            if (nuevaMemoria is Memoria memoria1)
            {
                memoria1.Megas = 30000;


                memoria1.Coste = 600;
                memoria1.Calor = 50;
                memoria1.NumeroSerie = "879-FH";
                Assert.AreEqual(30000, memoria1.GetMemoria());
                Assert.AreEqual(600, memoria1.GetPrecio());
                Assert.AreEqual(50, memoria1.GetTemperatura());
                Assert.AreEqual("879-FH", memoria1.NumeroSerie);

            }
        }


        [TestMethod]
        public void ClonarProcesadores()
        {
            IProcesable procesador = new Procesador("Procesador1",6, 30, 30,"CVCDLG");
            var nuevoProcesador = (procesador as IClonable)?.Clone();
            Assert.AreEqual(procesador.GetCores(), (nuevoProcesador as Procesador)?.GetCores());
            Assert.AreEqual(procesador.GetPrecio(), (nuevoProcesador as Procesador)?.GetPrecio());
            Assert.AreEqual(procesador.GetTemperatura(), (nuevoProcesador as Procesador)?.GetTemperatura());

        }
        [TestMethod]
        public void ClonarDiscos()
        {
            IMedible disco = new Disco("Disco1", 20, 30, 30, "CVCDLG");
            var nuevoDisco = (disco as IClonable)?.Clone();
            Assert.AreEqual(disco.GetMemoria(), (nuevoDisco as Disco)?.GetMemoria());
            Assert.AreEqual(disco.GetPrecio(), (nuevoDisco as Disco)?.GetPrecio());
            Assert.AreEqual(disco.GetTemperatura(), (nuevoDisco as Disco)?.GetTemperatura());

        }

        [TestMethod]
        public void CrearComponentesBuilder()
        {
            IComponenteBuilder componenteBuilder = new ComponenteBuilder();
            foreach (Memorias modeloMemoria in Enum.GetValues(typeof(Memorias)))
            {
                if (modeloMemoria == Memorias.None)
                    continue;
                var memoria = componenteBuilder.GetMemoria(modeloMemoria);
                Assert.IsNotNull(memoria);
            }
            foreach (Procesadores modeloProcesador in Enum.GetValues(typeof(Procesadores)))
            {
                if (modeloProcesador == Procesadores.None)
                    continue;
                var procesador = componenteBuilder.GetProcesador(modeloProcesador);
                Assert.IsNotNull(procesador);
            }
            foreach (Discos modeloDisco in Enum.GetValues(typeof(Discos)))
            {
                if (modeloDisco == Discos.None)
                    continue;
                var disco = componenteBuilder.GetDisco(modeloDisco);
                Assert.IsNotNull(disco);
            }
        }
        [TestMethod]
        public void ObtenerPrecioOrdenadorBuilder()
        {
            var ordenador = _builderOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorMaria);
            Assert.IsNotNull(ordenador);
            Assert.AreEqual(284, ordenador.GetPrecio());
            var ordenador2 = _builderOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorAndres);
            Assert.IsNotNull(ordenador2);
            Assert.AreEqual(556, ordenador2.GetPrecio());
        }
        [TestMethod]
        public void ObtenerTemperaturaOrdenadorBuilder()
        {
            var ordenador = _builderOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorMaria);
            Assert.IsNotNull(ordenador);
            Assert.AreEqual(30, ordenador.GetTemperatura());
            var ordenador2 = _builderOrdenador.DameOrdenador(TipoOrdenadores.OrdenadorAndres);
            Assert.IsNotNull(ordenador2);
            Assert.AreEqual(123, ordenador2.GetTemperatura());
        }
    }
}