using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Services.FakeRepositories
{
    public class FakeRepositoryFacturas : IFacturasRepository
    {
        private readonly List<Factura> _facturas = new();

        public FakeRepositoryFacturas()
        {
            _facturas.Add(
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
                });
            _facturas.Add(
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
                });

        }

        public Task Add(Factura factura)
        {
            if (factura.Id == 0)
                throw new Exception();
            _facturas.Add(factura);
            return Task.CompletedTask;
        }

        public Task<List<Factura>> All()
        {
            return Task.FromResult(_facturas);
        }

        public Task Delete(int id)
        {
            if(GetById(id).Result ==null)
                throw new Exception();
            _facturas.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteRange(int[] deleteInputs)
        {
            foreach (var id in deleteInputs)
            {
                _facturas.RemoveAll(p => p.Id == id);
            }

            return Task.CompletedTask;
        }

        public Task Edit(Factura factura)
        {
            var facturaAntiguo = GetById(factura.Id);
            if (facturaAntiguo.Result != null)
            {
                var indice = _facturas.IndexOf(facturaAntiguo.Result);
                _facturas[indice] = factura;
            }
            else
            {
                throw new Exception();
            }

            return Task.CompletedTask;
        }

        public Task<Factura?> GetById(int? id)
        {
            return Task.FromResult(_facturas.Find(p => p.Id == id));
        }
    }
}
