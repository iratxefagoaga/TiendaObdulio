using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Controllers;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services;
using MVC_ComponentesCodeFirst.Services.FakeRepositories;

namespace MVC_ComponentesCodeFirst.Test
{
    [TestClass]
    public class UnitTestFacturas
    {
        readonly FacturasController _controlador = new(new FakeRepositoryFacturas());
        private readonly OverridenEquals _comparador = new();
        [TestMethod]
        public void PruebaFacturasCrearVista()
        {
            var result = (ViewResult)_controlador.Create();
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void PruebaFacturasDetallesVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Details(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var factura = result.ViewData.Model as Factura;
            Assert.IsNotNull(factura);
            var expectedFactura = new Factura()
            {
                Id = 2,
                Descripcion = "Factura B",
                Fecha = DateTime.Today,
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 2,
                        Fecha = DateTime.Today,
                    }
                }
            };
            Assert.IsTrue(_comparador.FacturasIguales(expectedFactura,factura));
        }
        [TestMethod]
        public void PruebaFacturasIndexVistaOk()
        {
            var result = _controlador.Index().Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaFacturas = result.ViewData.Model as List<Factura>;
            Assert.IsNotNull(listaFacturas);
            Assert.AreEqual(2, listaFacturas.Count);
        }
        [TestMethod]
        public void PruebaFacturasEditVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Edit(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var factura = result.ViewData.Model as Factura;
            Assert.IsNotNull(factura); 
            var expectedFactura = new Factura()
            {
                Id = 2,
                Descripcion = "Factura B",
                Fecha = DateTime.Today,
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 2,
                        Fecha = DateTime.Today,
                    }
                }
            };
            Assert.IsTrue(_comparador.FacturasIguales(expectedFactura, factura));
        }
        [TestMethod]
        public void PruebaFacturasDeletetVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Delete(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var factura = result.ViewData.Model as Factura;
            Assert.IsNotNull(factura);
            var expectedFactura = new Factura()
            {
                Id = 2,
                Descripcion = "Factura B",
                Fecha = DateTime.Today,
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 2,
                        Fecha = DateTime.Today,
                    }
                }
            };
            Assert.IsTrue(_comparador.FacturasIguales(expectedFactura, factura));
        }
        [TestMethod]
        public void PruebaFacturasDeleteAccion()
        {
            var exists = _controlador.FacturaExists(2);
            Assert.IsTrue(exists);
            var result = _controlador.DeleteConfirmed(2);
            Assert.IsNotNull(result);
            exists = _controlador.FacturaExists(2);
            Assert.IsFalse(exists);
        }
        [TestMethod]
        public void PruebaFacturasDeleteAccionNoValido()
        {
            var result = _controlador.DeleteConfirmed(7).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
        }
        [TestMethod]
        public void PruebaFacturasBatchDeleteAccion()
        {
            var inputIds = new[] { 1, 2 };
            foreach (var id in inputIds)
            {
                var exists = _controlador.FacturaExists(id);
                Assert.IsTrue(exists);
            }

            var result = _controlador.BatchDelete(inputIds);
            Assert.IsNotNull(result);
            foreach (var id in inputIds)
            {
                var exists = _controlador.FacturaExists(id);
                Assert.IsFalse(exists);
            }
        }
        [TestMethod]
        public void PruebaFacturasCreateAccion()
        {
            Factura factura = new()
            {
                Descripcion = "Factura C",
                Fecha = DateTime.Today,
                Id = 3,
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 3,
                        Fecha = DateTime.Today,
                    },
                    new()
                    {
                        Id = 2,
                        ClienteId = 1,
                        Descripcion = "Pedido B",
                        FacturaId = 3,
                        Fecha = DateTime.Today,
                    }
                }
            };
            var result = _controlador.Create(factura);
            Assert.IsNotNull(result);
            Assert.IsTrue(_controlador.FacturaExists(3));
        }
        [TestMethod]
        public void PruebaFacturasCreateAccionNoValido()
        {
            Factura factura = new()
            {
                Descripcion = "Factura C",
                Fecha = DateTime.Today,
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 3,
                        Fecha = DateTime.Today,
                    },
                    new()
                    {
                        Id = 2,
                        ClienteId = 1,
                        Descripcion = "Pedido B",
                        FacturaId = 3,
                        Fecha = DateTime.Today,
                    }
                }
            };
            var result = _controlador.Create(factura).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Create" , result.ViewName);
        }
        [TestMethod]
        public void PruebaFacturasEditAccion()
        {
            Factura factura = new()
            {
                Id = 2,
                Descripcion = "Factura B cambiada",
                Fecha = DateTime.Now,
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 2,
                        Fecha = DateTime.Today,
                    }
                }
            };
            var facturaAntiguo = (_controlador.Details(factura.Id).Result as ViewResult)?.Model as Factura;
            Assert.IsNotNull(facturaAntiguo);
            var result = _controlador.Edit(factura);
            Assert.IsNotNull(result);
            var facturaModificado = (_controlador.Details(factura.Id).Result as ViewResult)?.Model as Factura;
            Assert.IsNotNull(facturaModificado);
            Assert.IsFalse(_comparador.FacturasIguales(facturaModificado,facturaAntiguo));
        }
        [TestMethod]
        public void PruebaFacturasEditAccionNoValido()
        {
            Factura factura = new()
            {
                Descripcion = "Factura B cambiada",
                Fecha = DateTime.Now,
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 2,
                        Fecha = DateTime.Today,
                    }
                }
            };
            var result = _controlador.Edit(factura).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
        }
    }
}
