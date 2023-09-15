using TiendaOrdenadoresAPI.Models;

namespace TiendaOrdenadoresAPI.Services.Interfaces
{
    public interface IFacturasRepository
    {
        public List<Factura>? All();
        public Factura? GetById(int? id);
        public void Add(Factura factura);
        public void Edit(Factura factura);
        public void Delete(int id);
    }
}
