using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Services.FakeRepositories
{
    public class FakeRepositoryCliente :IClienteRepository
    {
        private readonly List<Cliente> _clientes = new();

        public FakeRepositoryCliente()
        {
            _clientes.Add(
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
                });
            _clientes.Add(
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
                });

        }

        public void Add(Cliente cliente)
        {
            if (cliente.Id == 0)
                throw new Exception("Cliente no válido");
            _clientes.Add(cliente);
        }

        public List<Cliente> All()
        {
            return _clientes;
        }

        public void Delete(int id)
        {
            if (GetById(id) == null)
                throw new Exception("Cliente no existe");
            _clientes.RemoveAll(p => p.Id == id);
        }

        public void Edit(Cliente cliente)
        {
            var clienteAntiguo = GetById(cliente.Id);
            if (clienteAntiguo != null)
            {
                var indice = _clientes.IndexOf(clienteAntiguo);
                _clientes[indice] = cliente;
            }
            else
            {
                throw new Exception("Cliente no existe");
            }
        }

        public Cliente? GetById(int? id)
        {
            return _clientes.Find(p => p.Id == id);
        }
    }
}
