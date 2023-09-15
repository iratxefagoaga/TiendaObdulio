using Ejercicio_ordenadores.Builders.Componentes;
using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresAPI.Controllers;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services;
using TiendaOrdenadoresAPI.Services.FakeRepositories;

namespace TiendaOrdenadoresAPI.Test
{
    [TestClass]
    public class UnitTestClientes
    {
        readonly ClientesController _controlador = new(new FakeRepositoryCliente());
        private readonly OverridenEquals _comparador = new();

        [TestMethod]
        public void GetClientes()
        {
            List<Cliente> expectedListaClientes = new List<Cliente>()
            {
                new Cliente()
                {
                    Id = 1,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Email = "juanperez@gmail.com",
                    CreditCard = "1221434342134134",
                    Password = "contraseñaJuan",
                    Pedidos = new List<Pedido>()
                    {
                        new()
                        {
                            Id = 1,
                            ClienteId = 1,
                            Descripcion = "Pedido A",
                            FacturaId = 1,
                            Fecha = DateTime.Today
                        },
                        new()
                        {
                            Id = 2,
                            ClienteId = 1,
                            Descripcion = "Pedido B",
                            FacturaId = 1,
                            Fecha = DateTime.Today
                        }
                    }
                },
                new Cliente()
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
                }
            };
            var result = _controlador.GetClientes();
            IEnumerable<Cliente>? listaClientes = result.Value;
            
            Assert.IsNotNull(listaClientes);
            var clientes = listaClientes.ToList();
            Assert.IsTrue(_comparador.ClientesIguales(expectedListaClientes[0], clientes[0]));
            Assert.IsTrue(_comparador.ClientesIguales(expectedListaClientes[1], clientes[1]));
             }

        [TestMethod]
        public void GetClienteBien()
        {
            var result = _controlador.GetCliente(2);
            Cliente? cliente = _controlador.GetCliente(2).Value;
            Assert.IsNotNull(cliente);
            Cliente expectedCliente = new Cliente()
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
            Assert.IsTrue(_comparador.ClientesIguales(cliente, expectedCliente));
        }

        [TestMethod]
        public void GetClienteMal()
        {
            var result = _controlador.GetCliente(50);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostCliente()
        {
            Cliente cliente = new()
            {
                Nombre = "Mario",
                Apellido = "Fernandez",
                Password = "Contraseña",
                Id = 3,
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
            var result = _controlador.PostCliente(cliente);
            Assert.IsNotNull(result);
            var obtenerCliente = _controlador.GetCliente(3).Value;
            Assert.IsNotNull(obtenerCliente);
            Assert.IsTrue(_comparador.ClientesIguales(cliente, obtenerCliente));
        }
       
        [TestMethod]
        public void PutCliente()
        {
            Cliente cliente = new()
            {
                Nombre = "Mario",
                Apellido = "Fernandez cambiado",
                Password = "Contraseña",
                Id = 2,
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
            var clienteAntiguo = _controlador.GetCliente(cliente.Id).Value;
            Assert.IsNotNull(clienteAntiguo);
            var result = _controlador.PutCliente(cliente.Id, cliente);
            Assert.IsNotNull(result);
            var clienteModificado = _controlador.GetCliente(cliente.Id).Value;
            Assert.IsNotNull(clienteModificado);
           
            Assert.IsTrue(_comparador.ClientesIguales(clienteModificado, cliente));

        }

        [TestMethod]
        public void DeleteCliente()
        {
            var exists = _controlador.GetCliente(2).Value;
            Assert.IsNotNull(exists);
            var result = _controlador.DeleteCliente(2);
            Assert.IsNotNull(result);
            var noexists = _controlador.GetCliente(2).Result;
            Assert.IsInstanceOfType(noexists, typeof(NotFoundResult));
        }
    }
}