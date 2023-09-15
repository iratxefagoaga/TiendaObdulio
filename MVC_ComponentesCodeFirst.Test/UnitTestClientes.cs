using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Controllers;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services;
using MVC_ComponentesCodeFirst.Services.FakeRepositories;

namespace MVC_ComponentesCodeFirst.Test
{
    [TestClass]
    public class UnitTestClientes
    {
        readonly ClientesController _controlador = new(new FakeRepositoryClientes());
        private readonly OverridenEquals _comparador = new();
        [TestMethod]
        public void PruebaClientesCrearVista()
        {
            var result = (ViewResult)_controlador.Create();
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void PruebaClientesDetallesVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Details(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var cliente = result.ViewData.Model as Cliente;
            Assert.IsNotNull(cliente);
            var expectedCliente = new Cliente()
            {
                Id = 2,
                Nombre = "Maria",
                Apellido = "Perez",
                Email = "mariaperez@gmail.com",
                CreditCard = "122143434234134",
                Password = "contraseñaMaria",
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 1,
                        Fecha = DateTime.Today
                    },
                    new()
                    {
                        Id = 2,
                        ClienteId = 2,
                        Descripcion = "Pedido B",
                        FacturaId = 1,
                        Fecha = DateTime.Today
                    }
                }
            };
            Assert.IsTrue(_comparador.ClientesIguales(expectedCliente, cliente));
        }
        [TestMethod]
        public void PruebaClientesIndexVistaOk()
        {
            var result = _controlador.Index().Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaClientes = result.ViewData.Model as List<Cliente>;
            Assert.IsNotNull(listaClientes);
            Assert.AreEqual(2, listaClientes.Count);
        }
        [TestMethod]
        public void PruebaClientesEditVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Edit(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var cliente = result.ViewData.Model as Cliente;
            Assert.IsNotNull(cliente); 
            var expectedCliente = new Cliente()
            {
                Id = 2,
                Nombre = "Maria",
                Apellido = "Perez",
                Email = "mariaperez@gmail.com",
                CreditCard = "122143434234134",
                Password = "contraseñaMaria",
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 1,
                        Fecha = DateTime.Today
                    },
                    new()
                    {
                        Id = 2,
                        ClienteId = 2,
                        Descripcion = "Pedido B",
                        FacturaId = 1,
                        Fecha = DateTime.Today
                    }
                }
            };
            Assert.IsTrue(_comparador.ClientesIguales(expectedCliente, cliente));
        }
        [TestMethod]
        public void PruebaClientesDeletetVistaEncontrado()
        {
            var result = (ViewResult)_controlador.Delete(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var cliente = result.ViewData.Model as Cliente;
            Assert.IsNotNull(cliente);
            var expectedCliente = new Cliente()
            {
                Id = 2,
                Nombre = "Maria",
                Apellido = "Perez",
                Email = "mariaperez@gmail.com",
                CreditCard = "122143434234134",
                Password = "contraseñaMaria",
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 1,
                        Fecha = DateTime.Today
                    },
                    new()
                    {
                        Id = 2,
                        ClienteId = 2,
                        Descripcion = "Pedido B",
                        FacturaId = 1,
                        Fecha = DateTime.Today
                    }
                }
            };
            Assert.IsTrue(_comparador.ClientesIguales(expectedCliente, cliente));
        }
        [TestMethod]
        public void PruebaClientesDeleteAccion()
        {
            var exists = _controlador.ClienteExists(2);
            Assert.IsTrue(exists);
            var result = _controlador.DeleteConfirmed(2);
            Assert.IsNotNull(result);
            exists = _controlador.ClienteExists(2);
            Assert.IsFalse(exists);
        }
        [TestMethod]
        public void PruebaClientesDeleteAccionNoValido()
        {
            var result = _controlador.DeleteConfirmed(7).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
        }
        [TestMethod]
        public void PruebaClientesBatchDeleteAccion()
        {
            var inputIds = new[] { 1, 2 };
            foreach (var id in inputIds)
            {
                var exists = _controlador.ClienteExists(id);
                Assert.IsTrue(exists);
            }

            var result = _controlador.BatchDelete(inputIds);
            Assert.IsNotNull(result);
            foreach (var id in inputIds)
            {
                var exists = _controlador.ClienteExists(id);
                Assert.IsFalse(exists);
            }
        }
        [TestMethod]
        public void PruebaClientesCreateAccion()
        {
            Cliente cliente = new()
            {
                Nombre = "Mario",
                Apellido="Fernandez",
                Password="Contraseña",
                Id = 3,
                CreditCard="238429384",
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        FacturaId = 3,
                        Descripcion = "Pedido A",
                        ClienteId = 3,
                        Fecha = DateTime.Today
                    },
                    new()
                    {
                        Id = 2,
                        FacturaId = 1,
                        Descripcion = "Pedido B",
                        ClienteId = 3,
                        Fecha = DateTime.Today
                    }
                }
            };
            var result = _controlador.Create(cliente);
            Assert.IsNotNull(result);
            Assert.IsTrue(_controlador.ClienteExists(3));
        }
        [TestMethod]
        public void PruebaClientesCreateAccionNoValido()
        {
            Cliente cliente = new()
            {
                Nombre = "Mario",
                Apellido = "Fernandez",
                Password = "Contraseña",
                CreditCard = "238429384",
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        FacturaId = 3,
                        Descripcion = "Pedido A",
                        ClienteId = 3,
                        Fecha = DateTime.Today
                    },
                    new()
                    {
                        Id = 2,
                        FacturaId = 1,
                        Descripcion = "Pedido B",
                        ClienteId = 3,
                        Fecha = DateTime.Today
                    }
                }
            };
            var result = _controlador.Create(cliente).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void PruebaClientesEditAccion()
        {
            Cliente cliente = new()
            {
                Id = 2,
                Nombre = "Mario",
                Apellido = "Perez",
                Email = "mariaperez@gmail.com",
                CreditCard = "122143434234134",
                Password = "contraseñaMaria",
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 1,
                        Fecha = DateTime.Today
                    },
                    new()
                    {
                        Id = 2,
                        ClienteId = 2,
                        Descripcion = "Pedido B",
                        FacturaId = 1,
                        Fecha = DateTime.Today  
                    }
                }
            };
            var clienteAntiguo = (_controlador.Details(cliente.Id).Result as ViewResult)?.Model as Cliente;
            Assert.IsNotNull(clienteAntiguo);
            var result = _controlador.Edit(cliente);
            Assert.IsNotNull(result);
            var clienteModificado = (_controlador.Details(cliente.Id).Result as ViewResult)?.Model as Cliente;
            Assert.IsNotNull(clienteModificado);
            Assert.IsFalse(_comparador.ClientesIguales(clienteModificado, clienteAntiguo));
        }
        [TestMethod]
        public void PruebaClientesEditAccionNoValido()
        {
            Cliente cliente = new()
            {
                Nombre = "Mario",
                Apellido = "Perez",
                Email = "mariaperez@gmail.com",
                CreditCard = "122143434234134",
                Password = "contraseñaMaria",
                Pedidos = new List<Pedido>()
                {
                    new()
                    {
                        Id = 1,
                        ClienteId = 2,
                        Descripcion = "Pedido A",
                        FacturaId = 1,
                        Fecha = DateTime.Today
                    },
                    new()
                    {
                        Id = 2,
                        ClienteId = 2,
                        Descripcion = "Pedido B",
                        FacturaId = 1,
                        Fecha = DateTime.Today
                    }
                }
            };
           var result = _controlador.Edit(cliente).Result as ViewResult;
           Assert.IsNotNull(result);
           Assert.AreEqual("Edit", result.ViewName);
        }
    }
}
