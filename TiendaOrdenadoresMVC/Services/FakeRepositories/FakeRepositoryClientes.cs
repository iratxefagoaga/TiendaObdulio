using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Services.FakeRepositories
{
    public class FakeRepositoryClientes : IGenericRepository<Cliente>
    {
        private readonly List<Cliente> _clientes = new();

        public FakeRepositoryClientes()
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

        public Task Add(Cliente cliente)
        {
            if (cliente.Id == 0)
                throw new Exception("Cliente no válido");
            _clientes.Add(cliente);
            return Task.CompletedTask;
        }

        public Task<List<Cliente>> All()
        {
            return Task.FromResult(_clientes);
        }

        public Task Delete(int id)
        {
            if (GetById(id).Result == null)
                throw new Exception("Cliente no existe");
            _clientes.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteRange(int[] deleteInputs)
        {
            foreach (var id in deleteInputs)
            {
                _clientes.RemoveAll(p => p.Id == id);
            }

            return Task.CompletedTask;
        }

        public Task Edit(Cliente cliente)
        {
            var clienteAntiguo = GetById(cliente.Id);
            if (clienteAntiguo.Result != null)
            {
                var indice = _clientes.IndexOf(clienteAntiguo.Result);
                _clientes[indice] = cliente;
            }
            else
            {
                throw new Exception("Cliente no existe");
            }

            return Task.CompletedTask;
        }

        public Task<Cliente?> GetById(int? id)
        {
            return Task.FromResult(_clientes.Find(p => p.Id == id));
        }
    }
}
