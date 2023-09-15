using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresAPI.Controllers;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services;
using TiendaOrdenadoresAPI.Services.FakeRepositories;

namespace TiendaOrdenadoresAPI.Test
{
    [TestClass]
    public class UnitTestFacturas
    {
        readonly FacturasController _controlador = new(new FakeRepositoryFactura());
        private readonly OverridenEquals _comparador = new();

        [TestMethod]
        public void GetFacturas()
        {
            List<Factura> expectedListaFacturas = new()
            {
                new Factura()
                {
                    Id = 1,
                    Descripcion = "Factura A",
                    Fecha = DateTime.Today,
                    Pedidos = new List<Pedido>()
                    {
                        new()
                        {
                            Id = 1,
                            ClienteId = 2,
                            Descripcion = "Pedido A",
                            FacturaId = 1,
                            Fecha = DateTime.Today,
                        },
                        new()
                        {
                            Id = 2,
                            ClienteId = 1,
                            Descripcion = "Pedido B",
                            FacturaId = 1,
                            Fecha = DateTime.Today,
                        }
                    }
                },
                new Factura()
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
                }
            };
            var result = _controlador.GetFacturas();
            IEnumerable<Factura>? listaFacturas = result.Value;
            
            Assert.IsNotNull(listaFacturas);
            var facturas = listaFacturas.ToList();
            Assert.IsTrue(_comparador.FacturasIguales(expectedListaFacturas[0], facturas[0]));
            Assert.IsTrue(_comparador.FacturasIguales(expectedListaFacturas[1], facturas[1]));
        }

        [TestMethod]
        public void GetFactura()
        {
            var result = _controlador.GetFactura(2);
            Factura? factura = _controlador.GetFactura(2).Value;
            Assert.IsNotNull(factura);
            Factura expectedFactura = new Factura()
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
            Assert.IsTrue(_comparador.FacturasIguales(factura, expectedFactura));
        }

        [TestMethod]
        public void GetFacturaMal()
        {
            var result = _controlador.GetFactura(50);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostFactura()
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
            var result = _controlador.PostFactura(factura);
            Assert.IsNotNull(result);
            var obtenerFactura = _controlador.GetFactura(3).Value;
            Assert.IsNotNull(obtenerFactura);
            Assert.IsTrue(_comparador.FacturasIguales(factura, obtenerFactura));
        }
        [TestMethod]
        public void PutFactura()
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
            var facturaAntiguo = _controlador.GetFactura(factura.Id).Value;
            Assert.IsNotNull(facturaAntiguo);
            var result = _controlador.PutFactura(factura.Id, factura);
            Assert.IsNotNull(result);
            var facturaModificado = _controlador.GetFactura(factura.Id).Value;
           
            Assert.IsTrue(_comparador.FacturasIguales(facturaModificado, factura));

        }

        [TestMethod]
        public void DeleteFacturaBien()
        {
            var exists = _controlador.GetFactura(2).Value;
            Assert.IsNotNull(exists);
            var result = _controlador.DeleteFactura(2);
            Assert.IsNotNull(result);
            var noexists = _controlador.GetFactura(2).Result;
            Assert.IsInstanceOfType(noexists, typeof(NotFoundResult));
        }
    }
}