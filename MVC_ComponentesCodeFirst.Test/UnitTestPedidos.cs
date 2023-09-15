using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Controllers;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services;
using MVC_ComponentesCodeFirst.Services.FakeRepositories;

namespace MVC_ComponentesCodeFirst.Test
{
    [TestClass]
    public class UnitTestPedidos
    {
        readonly PedidosController _controlador = new(new FakeRepositoryPedidos());
        private readonly OverridenEquals _comparador = new();
        [TestMethod]
        public void PruebaPedidosCrearVista()
        {
            var result = (ViewResult)_controlador.Create();
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void PruebaPedidosDetallesVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Details(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var pedido = result.ViewData.Model as Pedido;
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
            Assert.IsTrue(_comparador.PedidosIguales(expectedPedido,pedido));
        }
        [TestMethod]
        public void PruebaPedidosIndexVistaOk()
        {
            var result = _controlador.Index().Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaPedidos = result.ViewData.Model as List<Pedido>;
            Assert.IsNotNull(listaPedidos);
            Assert.AreEqual(2, listaPedidos.Count);
        }
        [TestMethod]
        public void PruebaPedidosEditVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Edit(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var pedido = result.ViewData.Model as Pedido;
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
            Assert.IsTrue(_comparador.PedidosIguales(expectedPedido, pedido));
        }
        [TestMethod]
        public void PruebaPedidosDeletetVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Delete(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var pedido = result.ViewData.Model as Pedido;
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
            Assert.IsTrue(_comparador.PedidosIguales(expectedPedido, pedido));
        }
        [TestMethod]
        public void PruebaPedidosDeleteAccion()
        {
            var exists = _controlador.PedidoExists(2);
            Assert.IsTrue(exists);
            var result = _controlador.DeleteConfirmed(2);
            Assert.IsNotNull(result);
            exists = _controlador.PedidoExists(2);
            Assert.IsFalse(exists);
        }
        [TestMethod]
        public void PruebaPedidosDeleteAccionNoValido()
        {
            var result = _controlador.DeleteConfirmed(7).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
        }
        [TestMethod]
        public void PruebaPedidosBatchDeleteAccion()
        {
            var inputIds = new[] { 1, 2 };
            foreach (var id in inputIds)
            {
                var exists = _controlador.PedidoExists(id);
                Assert.IsTrue(exists);
            }

            var result = _controlador.BatchDelete(inputIds);
            Assert.IsNotNull(result);
            foreach (var id in inputIds)
            {
                var exists = _controlador.PedidoExists(id);
                Assert.IsFalse(exists);
            }
        }
        [TestMethod]
        public void PruebaPedidosCreateAccion()
        {
            Pedido pedido = new()
            {
                Descripcion = "Pedido C",
                ClienteId = 3,
                FacturaId = 3,
                Fecha = DateTime.Today,
                Id = 3
            };
            var result = _controlador.Create(pedido);
            Assert.IsNotNull(result);
            Assert.IsTrue(_controlador.PedidoExists(3));
        }
        [TestMethod]
        public void PruebaPedidosCreateAccionNoValido()
        {
            Pedido pedido = new()
            {
                Descripcion = "Pedido C",
                ClienteId = 3,
                FacturaId = 3,
                Fecha = DateTime.Today
            };
            var result = _controlador.Create(pedido).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void PruebaPedidosEditAccion()
        {
            Pedido pedido = new()
            {
                Id = 2,
                ClienteId = 3,
                Descripcion = "Pedido B",
                FacturaId = 1,
                Fecha = DateTime.Today,
            };
            var pedidoAntiguo = (_controlador.Details(pedido.Id).Result as ViewResult)?.Model as Pedido;
            Assert.IsNotNull(pedidoAntiguo);
            var result = _controlador.Edit(pedido);
            Assert.IsNotNull(result);
            var pedidoModificado = (_controlador.Details(pedido.Id).Result as ViewResult)?.Model as Pedido;
            Assert.IsNotNull(pedidoModificado);
            Assert.IsFalse(_comparador.PedidosIguales(pedidoModificado,pedidoAntiguo));
        }
        [TestMethod]
        public void PruebaPedidosEditAccionNoValido()
        {
            Pedido pedido = new()
            {
                ClienteId = 3,
                Descripcion = "Pedido B",
                FacturaId = 1,
                Fecha = DateTime.Today,
            };
            var result = _controlador.Edit(pedido).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
        }
    }
}
