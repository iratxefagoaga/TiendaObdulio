using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Controllers;
using MVC_ComponentesCodeFirst.Services;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.FakeRepositories;

namespace MVC_ComponentesCodeFirst.Test
{
    [TestClass]
    public class UnitTestOrdenadores
    {
        readonly OrdenadoresController _controlador = new(new FakeRepositoryOrdenadores());
        private readonly OverridenEquals _comparador = new();
        [TestMethod]
        public void PruebaOrdenadoresCrearVista()
        {
            var result = (ViewResult)_controlador.Create();
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void PruebaOrdenadoresDetallesVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Details(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var ordenador = result.ViewData.Model as Ordenador;
            Assert.IsNotNull(ordenador);
            var expectedOrdenador = new Ordenador()
            {
                Id = 2,
                Descripcion = "Ordenador 2",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new()
                    {
                        Id = 1,
                        Calor = 15,
                        Cores = 0,
                        Descripcion = "Memoria Ram 2",
                        Megas = 64,
                        Precio = 500,
                        Serie = "413",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM,
                        OrdenadorId = 2
                    },
                    new()
                    {
                        Id = 2,
                        Calor = 43,
                        Cores = 12,
                        Descripcion = "Procesador i7",
                        Megas = 0,
                        Precio = 122,
                        Serie = "2342",
                        TipoComponente = EnumTipoComponentes.Procesador,
                        OrdenadorId = 2
                    }
                },
                PedidoId = 2
            };
            Assert.IsTrue(_comparador.OrdenadoresIguales(expectedOrdenador, ordenador));
        }
        [TestMethod]
        public void PruebaOrdenadoresIndexVistaOk()
        {
            var result = _controlador.Index().Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaOrdenadores = result.ViewData.Model as List<Ordenador>;
            Assert.IsNotNull(listaOrdenadores);
            List<Ordenador> expectedOrdenadoresList = new()
            {
                new Ordenador()
                {
                    Id = 1,
                    Descripcion = "Ordenador 1",
                    Pedido = new Pedido()
                    {
                        Id = 1,
                        ClienteId = 1,
                        FacturaId = 1
                    },
                    Componentes = new List<Componente>()
                    {
                        new ()
                        {
                            Id = 1,
                            Calor = 12,
                            Cores = 0,
                            Descripcion = "Memoria Ram 1",
                            Megas = 30,
                            Precio = 500,
                            Serie = "ABC2",
                            TipoComponente = EnumTipoComponentes.MemoriaRAM,
                            OrdenadorId = 1
                        },
                        new ()
                        {
                            Id = 2,
                            Calor = 15,
                            Cores = 12,
                            Descripcion = "Procesador i5",
                            Megas = 0,
                            Precio = 60,
                            Serie = "ABC3",
                            TipoComponente = EnumTipoComponentes.Procesador,
                            OrdenadorId = 1
                        }
                    },
                    PedidoId = 1
                },
                new Ordenador()
                {
                    Id = 2,
                    Descripcion = "Ordenador 2",
                    Componentes = new List<Componente>()
                    {
                        new ()
                        {
                            Id = 1,
                            Calor = 15,
                            Cores = 0,
                            Descripcion = "Memoria Ram 2",
                            Megas = 64,
                            Precio = 500,
                            Serie = "413",
                            TipoComponente = EnumTipoComponentes.MemoriaRAM,
                            OrdenadorId = 2
                        },
                        new ()
                        {
                            Id = 2,
                            Calor = 43,
                            Cores = 12,
                            Descripcion = "Procesador i7",
                            Megas = 0,
                            Precio = 122,
                            Serie = "2342",
                            TipoComponente = EnumTipoComponentes.Procesador,
                            OrdenadorId = 2
                        }
                    },
                    PedidoId = 2
                }
            };
            Assert.IsTrue(_comparador.OrdenadoresIguales(expectedOrdenadoresList[0], expectedOrdenadoresList[0]));
            Assert.IsTrue(_comparador.OrdenadoresIguales(expectedOrdenadoresList[1], expectedOrdenadoresList[1]));
            }
        [TestMethod]
        public void PruebaOrdenadoresEditVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Edit(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var ordenador = result.ViewData.Model as Ordenador;
            Assert.IsNotNull(ordenador);
            var expectedOrdenador = new Ordenador()
            {
                Id = 2,
                Descripcion = "Ordenador 2",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new()
                    {
                        Id = 1,
                        Calor = 15,
                        Cores = 0,
                        Descripcion = "Memoria Ram 2",
                        Megas = 64,
                        Precio = 500,
                        Serie = "413",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM,
                        OrdenadorId = 2
                    },
                    new()
                    {
                        Id = 2,
                        Calor = 43,
                        Cores = 12,
                        Descripcion = "Procesador i7",
                        Megas = 0,
                        Precio = 122,
                        Serie = "2342",
                        TipoComponente = EnumTipoComponentes.Procesador,
                        OrdenadorId = 2
                    }
                },
                PedidoId = 2
            };
            Assert.IsTrue(_comparador.OrdenadoresIguales(expectedOrdenador, ordenador));
        }
        [TestMethod]
        public void PruebaOrdenadoresDeletetVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Delete(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var ordenador = result.ViewData.Model as Ordenador;
            Assert.IsNotNull(ordenador);
            var expectedOrdenador = new Ordenador()
            {
                Id = 2,
                Descripcion = "Ordenador 2",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new()
                    {
                        Id = 1,
                        Calor = 15,
                        Cores = 0,
                        Descripcion = "Memoria Ram 2",
                        Megas = 64,
                        Precio = 500,
                        Serie = "413",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM,
                        OrdenadorId = 2
                    },
                    new()
                    {
                        Id = 2,
                        Calor = 43,
                        Cores = 12,
                        Descripcion = "Procesador i7",
                        Megas = 0,
                        Precio = 122,
                        Serie = "2342",
                        TipoComponente = EnumTipoComponentes.Procesador,
                        OrdenadorId = 2
                    }
                },
                PedidoId = 2
            };
            Assert.IsTrue(_comparador.OrdenadoresIguales(expectedOrdenador, ordenador));
        }
        [TestMethod]
        public void PruebaOrdenadoresDeleteAccion()
        {
            var exists = _controlador.OrdenadorExists(2);
            Assert.IsTrue(exists);
            var result = _controlador.DeleteConfirmed(2);
            Assert.IsNotNull(result);
            exists = _controlador.OrdenadorExists(2);
            Assert.IsFalse(exists);
        }
        [TestMethod]
        public void PruebaOrdenadoresDeleteAccionNoValido()
        {
            var result = _controlador.DeleteConfirmed(7).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
        }
        [TestMethod]
        public void PruebaOrdenadoresBatchDeleteAccion()
        {
            var inputIds = new[] { 1, 2 };
            foreach (var id in inputIds)
            {
                var exists = _controlador.OrdenadorExists(id);
                Assert.IsTrue(exists);
            }

            var result = _controlador.BatchDelete(inputIds);
            Assert.IsNotNull(result);
            foreach (var id in inputIds)
            {
                var exists = _controlador.OrdenadorExists(id);
                Assert.IsFalse(exists);
            }
        }
        [TestMethod]
        public void PruebaOrdenadoresCreateAccion()
        {
            Ordenador ordenador = new ()
            {
                Id = 3,
                Descripcion = "Ordenador 3",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new()
                    {
                        Id = 4,
                        Calor = 12,
                        Cores = 0,
                        Descripcion = "Componente añadido",
                        Megas = 30,
                        Precio = 500,
                        Serie = "ABC2",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM
                    }
                }
            };
            var result = _controlador.Create(ordenador);
            Assert.IsNotNull(result);
            Assert.IsTrue(_controlador.OrdenadorExists(3));
        }
        [TestMethod]
        public void PruebaOrdenadoresCreateAccionNoValido()
        {
            Ordenador ordenador = new()
            {
                Descripcion = "Ordenador 3",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new()
                    {
                        Id = 4,
                        Calor = 12,
                        Cores = 0,
                        Descripcion = "Componente añadido",
                        Megas = 30,
                        Precio = 500,
                        Serie = "ABC2",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM
                    }
                }
            };
            var result = _controlador.Create(ordenador).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void PruebaOrdenadoresEditAccion()
        {
            Ordenador ordenador = new ()
            {
                Id = 2,
                Descripcion = "Ordenador 2 cambiado",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new()
                    {
                        Id = 4,
                        Calor = 12,
                        Cores = 0,
                        Descripcion = "Componente añadido",
                        Megas = 30,
                        Precio = 500,
                        Serie = "ABC2",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM
                    }
                }
            };
            var ordenadorAntiguo = (_controlador.Details(ordenador.Id).Result as ViewResult)?.Model as Ordenador;
            Assert.IsNotNull(ordenadorAntiguo);
            var result = _controlador.Edit(ordenador.Id, ordenador);
            Assert.IsNotNull(result);
            var ordenadorModificado = (_controlador.Details(ordenador.Id).Result as ViewResult)?.Model as Ordenador;
            Assert.IsNotNull(ordenadorModificado);
            var expectedOrdenador = new Ordenador()
            {
                Id = 2,
                Descripcion = "Ordenador 2",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new()
                    {
                        Id = 1,
                        Calor = 15,
                        Cores = 0,
                        Descripcion = "Memoria Ram 2",
                        Megas = 64,
                        Precio = 500,
                        Serie = "413",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM,
                        OrdenadorId = 2
                    },
                    new()
                    {
                        Id = 2,
                        Calor = 43,
                        Cores = 12,
                        Descripcion = "Procesador i7",
                        Megas = 0,
                        Precio = 122,
                        Serie = "2342",
                        TipoComponente = EnumTipoComponentes.Procesador,
                        OrdenadorId = 2
                    }
                },
                PedidoId = 2
            };
            Assert.IsFalse(_comparador.OrdenadoresIguales(expectedOrdenador, ordenadorModificado));
        }
        [TestMethod]
        public void PruebaOrdenadoresEditAccionNoValido()
        {
            Ordenador ordenador = new()
            {
                Descripcion = "Ordenador 2 cambiado",
                Pedido = new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    FacturaId = 1
                },
                Componentes = new List<Componente>()
                {
                    new()
                    {
                        Id = 4,
                        Calor = 12,
                        Cores = 0,
                        Descripcion = "Componente añadido",
                        Megas = 30,
                        Precio = 500,
                        Serie = "ABC2",
                        TipoComponente = EnumTipoComponentes.MemoriaRAM
                    }
                }
            };
           
            var result = _controlador.Edit(ordenador.Id, ordenador).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
        }
    }
}
