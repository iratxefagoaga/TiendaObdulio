using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Controllers;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services;
using MVC_ComponentesCodeFirst.Services.FakeRepositories;

namespace MVC_ComponentesCodeFirst.Test
{
    [TestClass]
    public class UnitTestComponentes
    {
        readonly ComponentesController _controlador = new(new FakeRepositoryComponentes());
        private readonly OverridenEquals _comparador = new();
        [TestMethod]
        public void PruebaComponentesCrearVista()
        {
            var result = (ViewResult)_controlador.Create();
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void PruebaComponentesDetallesVistaEncontrado()
        {
            var result = _controlador.Details(2).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var componente = result.ViewData.Model as Componente;
            Assert.IsNotNull(componente);
            Componente expectedComponente = new()
            {
                Id = 2,
                Calor = 12,
                Cores = 0,
                Descripcion = "Memoria Ram 1",
                Megas = 30,
                Precio = 500,
                Serie = "ABC2",
                TipoComponente = EnumTipoComponentes.MemoriaRAM,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            };
            Assert.IsTrue(_comparador.ComponentesIguales(componente,expectedComponente));
        }
        [TestMethod]
        public void PruebComponentesIndexVistaOk()
        {
            var result = _controlador.Index().Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaComponentes = result.ViewData.Model as List<Componente>;
            Assert.IsNotNull(listaComponentes);
            List<Componente> expectedListaComponentes = new()
            {
                new Componente()
                {
                    Id = 1,
                    Calor = 10,
                    Cores = 12,
                    Descripcion = "Procesador i7",
                    Megas = 0,
                    Precio = 130,
                    Serie = "ABC1",
                    TipoComponente = EnumTipoComponentes.Procesador,
                    OrdenadorId = 1,
                    Ordenador = new Ordenador()
                    {
                        Id = 1,
                        PedidoId = 1
                    }
                },
                new Componente()
                {
                    Id = 2,
                    Calor = 12,
                    Cores = 0,
                    Descripcion = "Memoria Ram 1",
                    Megas = 30,
                    Precio = 500,
                    Serie = "ABC2",
                    TipoComponente = EnumTipoComponentes.MemoriaRAM,
                    OrdenadorId = 1,
                    Ordenador = new Ordenador()
                    {
                        Id = 1,
                        PedidoId = 1
                    }
                },
                new Componente()
                {
                    Id = 3,
                    Calor = 15,
                    Cores = 12,
                    Descripcion = "Procesador i5",
                    Megas = 0,
                    Precio = 60,
                    Serie = "ABC3",
                    TipoComponente = EnumTipoComponentes.Procesador,
                    OrdenadorId = 1,
                    Ordenador = new Ordenador()
                    {
                        Id = 1,
                        PedidoId = 1
                    }
                }
            };
            Assert.IsTrue(_comparador.ComponentesIguales(expectedListaComponentes[0], listaComponentes[0]));
            Assert.IsTrue(_comparador.ComponentesIguales(expectedListaComponentes[1], listaComponentes[1]));
            Assert.IsTrue(_comparador.ComponentesIguales(expectedListaComponentes[2], listaComponentes[2]));
        }
        [TestMethod]
        public void PruebaComponentesEditVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Edit(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var componente = result.ViewData.Model as Componente;
            Assert.IsNotNull(componente);
            Componente expectedComponente = new()
            {
                Id = 2,
                Calor = 12,
                Cores = 0,
                Descripcion = "Memoria Ram 1",
                Megas = 30,
                Precio = 500,
                Serie = "ABC2",
                TipoComponente = EnumTipoComponentes.MemoriaRAM,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            };
            Assert.IsTrue(_comparador.ComponentesIguales(componente, expectedComponente));
        }
        [TestMethod]
        public void PruebaComponentesDeletetVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Delete(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var componente = result.ViewData.Model as Componente;
            Assert.IsNotNull(componente);
            Componente expectedComponente = new()
            {
                Id = 2,
                Calor = 12,
                Cores = 0,
                Descripcion = "Memoria Ram 1",
                Megas = 30,
                Precio = 500,
                Serie = "ABC2",
                TipoComponente = EnumTipoComponentes.MemoriaRAM,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            };
            Assert.IsTrue(_comparador.ComponentesIguales(componente, expectedComponente));
        }
        [TestMethod]
        public void PruebaComponentesDeleteAccion()
        {
            var exists = _controlador.ComponenteExists(2);
            Assert.IsTrue(exists);
            var result = _controlador.DeleteConfirmed(2);
            Assert.IsNotNull(result);
            exists = _controlador.ComponenteExists(2);
            Assert.IsFalse(exists);
        }
        [TestMethod]
        public void PruebaComponentesDeleteAccionNoValido()
        {
            var result = _controlador.DeleteConfirmed(7).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete" , result.ViewName);
        }

        [TestMethod]
        public void PruebaComponentesBatchDeleteAccion()
        {
            var inputIds = new[] { 1, 2, 3 };
            foreach (var id in inputIds)
            {
                var exists = _controlador.ComponenteExists(id);


                Assert.IsTrue(exists);
            }

            var result = _controlador.BatchDelete(inputIds);
            Assert.IsNotNull(result);
            foreach (var id in inputIds)
            {
                var exists = _controlador.ComponenteExists(id);
                Assert.IsFalse(exists);
            }
        }
        [TestMethod]
        public void PruebaComponentesCreateAccion()
        {
            Componente componente = new ()
            {
                Id = 4,
                Calor = 12,
                Cores = 0,
                Descripcion = "Componente añadido",
                Megas = 30,
                Precio = 500,
                Serie = "ABC2",
                TipoComponente = EnumTipoComponentes.MemoriaRAM,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            };
            var result = _controlador.Create(componente);
            Assert.IsNotNull(result);
            Assert.IsTrue(_controlador.ComponenteExists(4));
        }
        [TestMethod]
        public void PruebaComponentesCreateAccionNoValido()
        {
            Componente componente = new()
            {
                Calor = 12,
                Cores = 0,
                Descripcion = "Componente añadido",
                Megas = 30,
                Precio = 500,
                Serie = "ABC2",
                TipoComponente = EnumTipoComponentes.MemoriaRAM,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            };
            var result = _controlador.Create(componente).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Create" ,result.ViewName);
        }
        [TestMethod]
        public void PruebaComponentesEditAccion()
        {
            Componente componente = new ()
            {
                Id = 1,
                Calor = 12,
                Cores = 0,
                Descripcion = "Componente cambiado",
                Megas = 30,
                Precio = 500,
                Serie = "ABC2",
                TipoComponente = EnumTipoComponentes.MemoriaRAM,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            };
            var componenteAntiguo = (_controlador.Details(componente.Id).Result as ViewResult)?.Model as Componente;
            Assert.IsNotNull(componenteAntiguo);
            var result = _controlador.Edit(componente);
            Assert.IsNotNull(result);
            var componenteModificado = (_controlador.Details(componente.Id).Result as ViewResult)?.Model as Componente;
            Assert.IsNotNull(componenteModificado);
            Componente expectedComponente = new()
            {
                Id = 2,
                Calor = 12,
                Cores = 0,
                Descripcion = "Memoria Ram 1",
                Megas = 30,
                Precio = 500,
                Serie = "ABC2",
                TipoComponente = EnumTipoComponentes.MemoriaRAM,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            };
            Assert.IsFalse(_comparador.ComponentesIguales(componenteModificado, expectedComponente));

        }
        [TestMethod]
        public void PruebaComponentesEditAccionNoValido()
        {
            Componente componente = new()
            {
                Calor = 12,
                Cores = 0,
                Descripcion = "Componente cambiado",
                Megas = 30,
                Precio = 500,
                Serie = "ABC2",
                TipoComponente = EnumTipoComponentes.MemoriaRAM,
                OrdenadorId = 1,
                Ordenador = new Ordenador()
                {
                    Id = 1,
                    PedidoId = 1
                }
            };
            
            var result = _controlador.Edit(componente).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
        }
    }
}