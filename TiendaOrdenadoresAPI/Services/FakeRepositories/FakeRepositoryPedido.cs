using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Services.FakeRepositories
{
    public class FakeRepositoryPedido :IPedidoRepository
    {
        private readonly List<Pedido> _pedidos = new();
        private readonly List<int> _facturasLista = new() { 1, 2, 3, 4 };
        private readonly List<int> _clientesLista = new() { 1, 2, 3, 4 };

        public FakeRepositoryPedido()
        {
            _pedidos.Add(
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
                });
            _pedidos.Add(
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
                });

        }
        public void Add(Pedido pedido)
        {
            if (pedido.Id == 0)
                throw new Exception();
            _pedidos.Add(pedido);
        }

        public List<Pedido> All()
        {
            return _pedidos;
        }

        public void Delete(int id)
        {
            if (GetById(id) == null)
                throw new Exception();
            _pedidos.RemoveAll(p => p.Id == id);
        }

        public void Edit(Pedido pedido)
        {
            var pedidoAntiguo = GetById(pedido.Id);
            if (pedidoAntiguo != null)
            {
                var indice = _pedidos.IndexOf(pedidoAntiguo);
                _pedidos[indice] = pedido;
            }
            else
            {
                throw new Exception();
            }
        }

        public Pedido? GetById(int? id)
        {
            return _pedidos.Find(p => p.Id == id);
        }
    }
}
