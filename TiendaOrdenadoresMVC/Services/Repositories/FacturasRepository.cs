using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Logging;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Services.Repositories
{
    public class FacturasRepository : IFacturasRepository
    {
        private readonly OrdenadoresContext _context;
        private readonly FactoriaContextos _factoriaDeContextos = new();
        private readonly ILoggerManager _loggerManager;
        public FacturasRepository(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _context = _factoriaDeContextos.CreateDbContext(new string[1]);
        }

        public async Task Add(Factura factura)
        {
            try
            {
                _context.Facturas.Add(factura);
                _loggerManager.LogDebug(
                    $"Factura añadida. Descripcion: {factura.Descripcion}");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al añadir factura. Message: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Factura>> All()
        {
            return _context.Facturas.Any()
                ? await _context.Facturas
                    .Include(f => f.Pedidos)!
                    .ThenInclude(p => p.Ordenadores)!
                    .ThenInclude(o => o.Componentes).ToListAsync()
                : new List<Factura>();
        }

        public async Task Delete(int id)
        {
            try
            {
                var factura = GetById(id);
                if (factura.Result != null)
                {
                    _context.Facturas.Remove(factura.Result);
                    _loggerManager.LogDebug(
                        $"Factura actualizado. Descripcion:  {factura.Result.Descripcion}");

                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar pedido. Message: " + ex.Message);
                throw;
            }
        }

        public async Task DeleteRange(int[] deleteInputs)
        {
            try
            {
                foreach (var id in deleteInputs)
                {
                    var factura = GetById(id);

                    if (factura.Result == null) continue;
                    _context.Facturas.Remove(factura.Result);
                    _loggerManager.LogDebug(
                        $"Factura eliminada. Descripcion:   {factura.Result.Descripcion}");
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar factura. Message: " + ex.Message);
                throw;
            }
        }

        public async Task Edit(Factura factura)
        {
            try
            {
                _context.Update(factura);
                _loggerManager.LogDebug(
                    $"Factura actualizada. Descripcion: {factura.Descripcion}");

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al editar factura. Message: " + ex.Message);
                throw;
            }
        }

        public async Task<Factura?> GetById(int? id)
        {
            if (_context.Facturas.Any() && id != null)
                return  await _context.Facturas
                    .Include(f => f.Pedidos)!
                    .ThenInclude(p => p.Ordenadores)!
                    .ThenInclude(o => o.Componentes).FirstOrDefaultAsync(m => m.Id == id);
            return null;
        }
    }
}
