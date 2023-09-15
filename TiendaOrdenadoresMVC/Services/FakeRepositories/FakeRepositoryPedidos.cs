using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Services.FakeRepositories
{
    public class FakeRepositoryPedidos : IPedidoRepository
    {
        private readonly List<Pedido> _pedidos = new();
        private readonly List<int> _facturasLista = new() { 1, 2, 3, 4 };
        private readonly List<int> _clientesLista = new() { 1, 2, 3, 4 };

        public FakeRepositoryPedidos()
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
        public Task Add(Pedido pedido)
        {
            if (pedido.Id == 0)
                throw new Exception();
            _pedidos.Add(pedido);
            return Task.CompletedTask;
        }

        public Task<List<Pedido>> All()
        {
            return Task.FromResult(_pedidos);
        }

        public Task Delete(int id)
        {
            if(GetById(id).Result == null)
                throw new Exception();
            _pedidos.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteRange(int[] deleteInputs)
        {
            foreach (var id in deleteInputs)
            {
                _pedidos.RemoveAll(p => p.Id == id);
            }

            return Task.CompletedTask;
        }

        public Task Edit(Pedido pedido)
        {
            var pedidoAntiguo = GetById(pedido.Id);
            if (pedidoAntiguo.Result != null)
            {
                var indice = _pedidos.IndexOf(pedidoAntiguo.Result);
                _pedidos[indice] = pedido;
            }
            else
            {
                throw new Exception();
            }

            return Task.CompletedTask;
        }

        public Task<Pedido?> GetById(int? id)
        {
            return Task.FromResult(_pedidos.Find(p => p.Id == id));
        }

        public SelectList ListaClientesId(int id = 0)
        {
            return new SelectList(_clientesLista);
        }

        public SelectList ListaFacturasId(int id = 0)
        {
            return new SelectList(_facturasLista);
        }
    }
}
