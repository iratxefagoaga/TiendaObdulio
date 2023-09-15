using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresAPI.Controllers;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services;
using TiendaOrdenadoresAPI.Services.FakeRepositories;

namespace TiendaOrdenadoresAPI.Test
{
    [TestClass]
    public class UnitTestOrdenadores
    {
        readonly OrdenadoresController _controlador = new(new FakeRepositoryOrdenador());
        private readonly OverridenEquals _comparador = new();

        [TestMethod]
        public void GetOrdenadores()
        {
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
                        new()
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
                        new()
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
                }
            };
            var result = _controlador.GetOrdenadores();
            IEnumerable<Ordenador>? listaOrdenadores = result.Value;
            
            Assert.IsNotNull(listaOrdenadores);
            var ordenadores = listaOrdenadores.ToList();
            Assert.IsTrue(_comparador.OrdenadoresIguales(expectedOrdenadoresList[0], ordenadores[0]));
            Assert.IsTrue(_comparador.OrdenadoresIguales(expectedOrdenadoresList[1], ordenadores[1]));
        }

        [TestMethod]
        public void GetOrdenadorBien()
        {
            var result = _controlador.GetOrdenador(2);
            Ordenador? ordenador = _controlador.GetOrdenador(2).Value;
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
            Assert.IsTrue(_comparador.OrdenadoresIguales(ordenador, expectedOrdenador));
        }

        [TestMethod]
        public void GetOrdenadorMal()
        {
            var result = _controlador.GetOrdenador(50);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostOrdenadorBien()
        {
            Ordenador ordenador = new()
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
            var result = _controlador.PostOrdenador(ordenador);
            Assert.IsNotNull(result);
            var obtenerOrdenador = _controlador.GetOrdenador(3).Value;
            Assert.IsNotNull(obtenerOrdenador);
            Assert.IsTrue(_comparador.OrdenadoresIguales(ordenador, obtenerOrdenador));
        }
        [TestMethod]
        public void PostOrdenadorMal()
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
            var result = _controlador.PostOrdenador(ordenador);
            Assert.IsNotNull(result);
            var obtenerOrdenador = _controlador.GetOrdenador(4).Value;
            Assert.IsNull(obtenerOrdenador);
            }

        [TestMethod]
        public void PutOrdenador()
        {
            Ordenador ordenador = new()
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
            var ordenadorAntiguo = _controlador.GetOrdenador(ordenador.Id).Value;
            Assert.IsNotNull(ordenadorAntiguo);
            var result = _controlador.PutOrdenador(ordenador.Id, ordenador);
            Assert.IsNotNull(result);
            var ordenadorModificado = _controlador.GetOrdenador(ordenador.Id).Value;
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
            Assert.IsFalse(_comparador.OrdenadoresIguales(ordenadorModificado, expectedOrdenador));

        }

        [TestMethod]
        public void DeleteOrdenadorBien()
        {
            var exists = _controlador.GetOrdenador(2).Value;
            Assert.IsNotNull(exists);
            var result = _controlador.DeleteOrdenador(2);
            Assert.IsNotNull(result);
            var noexists = _controlador.GetOrdenador(2).Result;
            Assert.IsInstanceOfType(noexists, typeof(NotFoundResult));
        }
    }
}