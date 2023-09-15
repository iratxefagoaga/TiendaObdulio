using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresAPI.Controllers;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services;
using TiendaOrdenadoresAPI.Services.FakeRepositories;

namespace TiendaOrdenadoresAPI.Test
{
    [TestClass]
    public class UnitTestPedidos
    {
        readonly PedidosController _controlador = new(new FakeRepositoryPedido());
        private readonly OverridenEquals _comparador = new();

        [TestMethod]
        public void GetPedidos()
        {
            List<Pedido> expectedListaPedidos = new List<Pedido>(){
                new Pedido()
                {
                    Id = 1,
                    ClienteId = 1,
                    Descripcion = "Pedido A",
                    FacturaId = 1,
                    Fecha = DateTime.Today,
                    Cliente = new Cliente()
                    {
                        Id = 1
                    },
                    Ordenadores = new List<Ordenador>()
                    {
                        new()
                        {
                            Id = 1,
                            PedidoId=1
                        },
                        new()
                        {
                            Id = 2,
                            PedidoId=1
                        }
                    },
                    Factura = new Factura()
                    {
                        Id = 1,
                        Fecha = DateTime.Today
                    }
                },
                new Pedido()
                {
                    Id = 2,
                    ClienteId = 2,
                    Descripcion = "Pedido B",
                    FacturaId = 1,
                    Fecha = DateTime.Today,
                    Cliente = new Cliente()
                    {
                        Id = 1
                    },
                    Ordenadores = new List<Ordenador>()
                    {
                        new()
                        {
                            Id = 1,
                            PedidoId = 2
                        },
                        new()
                        {
                            Id = 2,
                            PedidoId = 2
                        }
                    },
                    Factura = new Factura()
                    {
                        Id = 1,
                        Fecha = DateTime.Today
                    }
                }};
            var result = _controlador.GetPedidos();
            IEnumerable<Pedido>? listaPedidos = result.Value;

            Assert.IsNotNull(listaPedidos);
            var pedidos = listaPedidos.ToList();
            Assert.IsTrue(_comparador.PedidosIguales(expectedListaPedidos[0], pedidos[0]));
            Assert.IsTrue(_comparador.PedidosIguales(expectedListaPedidos[1], pedidos[1]));
        }

        [TestMethod]
        public void GetPedidoBien()
        {
            var result = _controlador.GetPedido(2);
            Pedido? pedido = _controlador.GetPedido(2).Value;
            Assert.IsNotNull(pedido);
            var expectedPedido = new Pedido()
            {
                Id = 2,
                ClienteId = 2,
                Descripcion = "Pedido B",
                FacturaId = 1,
                Fecha = DateTime.Today,
                Cliente = new Cliente()
                {
                    Id = 1
                },
                Ordenadores = new List<Ordenador>()
                {
                    new()
                    {
                        Id = 1,
                        PedidoId = 2
                    },
                    new()
                    {
                        Id = 2,
                        PedidoId = 2
                    }
                },
                Factura = new Factura()
                {
                    Id = 1,
                    Fecha = DateTime.Today
                }
            };
            Assert.IsTrue(_comparador.PedidosIguales(pedido, expectedPedido));
        }

        [TestMethod]
        public void GetPedidoMal()
        {
            var result = _controlador.GetPedido(50);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostPedidoBien()
        {
            Pedido pedido = new Pedido()
            {
                Id = 3,
                ClienteId = 2,
                Descripcion = "Pedido V",
                FacturaId = 1,
                Fecha = DateTime.Today,
                Cliente = new Cliente()
                {
                    Id = 1
                },
                Ordenadores = new List<Ordenador>()
                {
                    new()
                    {
                        Id = 1,
                        PedidoId = 2
                    },
                    new()
                    {
                        Id = 2,
                        PedidoId = 2
                    }
                },
                Factura = new Factura()
                {
                    Id = 1,
                    Fecha = DateTime.Today
                }
            };
            var result = _controlador.PostPedido(pedido);
            Assert.IsNotNull(result);
            var obtenerPedido = _controlador.GetPedido(3).Value;
            Assert.IsNotNull(obtenerPedido);
            Assert.IsTrue(_comparador.PedidosIguales(pedido, obtenerPedido));
        }
        [TestMethod]
        public void PutPedido()
        {
            Pedido pedido = new Pedido()
            {
                Id = 2,
                ClienteId = 2,
                Descripcion = "Pedido V cambiado",
                FacturaId = 1,
                Fecha = DateTime.Today,
                Cliente = new Cliente()
                {
                    Id = 1
                },
                Ordenadores = new List<Ordenador>()
                {
                    new()
                    {
                        Id = 1,
                        PedidoId = 2
                    },
                    new()
                    {
                        Id = 2,
                        PedidoId = 2
                    }
                },
                Factura = new Factura()
                {
                    Id = 1,
                    Fecha = DateTime.Today
                }
            };
            var pedidoAntiguo = _controlador.GetPedido(pedido.Id).Value;
            Assert.IsNotNull(pedidoAntiguo);
            var result = _controlador.PutPedido(pedido.Id, pedido);
            Assert.IsNotNull(result);
            var pedidoModificado = _controlador.GetPedido(pedido.Id).Value;
            Assert.IsNotNull(pedidoModificado);
           
            Assert.IsTrue(_comparador.PedidosIguales(pedidoModificado, pedido));

        }

        [TestMethod]
        public void DeleteOrdenadorBien()
        {
            var exists = _controlador.GetPedido(2).Value;
            Assert.IsNotNull(exists);
            var result = _controlador.DeletePedido(2);
            Assert.IsNotNull(result);
            var noexists = _controlador.GetPedido(2).Result;
            Assert.IsInstanceOfType(noexists, typeof(NotFoundResult));
        }
    }
}
