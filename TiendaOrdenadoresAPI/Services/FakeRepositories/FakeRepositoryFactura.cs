using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Services.FakeRepositories
{
    public class FakeRepositoryFactura : IFacturasRepository
    {
        private readonly List<Factura> _facturas = new();

        public FakeRepositoryFactura()
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

        public void Add(Factura factura)
        {
            if (factura.Id == 0)
                throw new Exception();
            _facturas.Add(factura);
        }

        public List<Factura> All()
        {
            return _facturas;
        }

        public void Delete(int id)
        {
            if (GetById(id) == null)
                throw new Exception();
            _facturas.RemoveAll(p => p.Id == id);
        }

        public void Edit(Factura factura)
        {
            var facturaAntiguo = GetById(factura.Id);
            if (facturaAntiguo != null)
            {
                var indice = _facturas.IndexOf(facturaAntiguo);
                _facturas[indice] = factura;
            }
            else
            {
                throw new Exception();
            }
        }

        public Factura? GetById(int? id)
        {
            return _facturas.Find(p => p.Id == id);
        }
    }
}
