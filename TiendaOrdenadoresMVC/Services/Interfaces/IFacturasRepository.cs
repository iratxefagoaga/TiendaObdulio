using MVC_ComponentesCodeFirst.Models;

namespace MVC_ComponentesCodeFirst.Services.Interfaces
{
    public interface IFacturasRepository
    {
        public Task<List<Factura>> All();
        public Task<Factura?> GetById(int? id);
        public Task Add(Factura factura);
        public Task Edit(Factura factura);
        public Task Delete(int id);
        public Task DeleteRange(int[] deleteInputs);
    }
}
