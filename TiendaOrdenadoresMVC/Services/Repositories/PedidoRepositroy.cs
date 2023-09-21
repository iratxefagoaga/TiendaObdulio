using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Logging;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Pedido = MVC_ComponentesCodeFirst.Models.Pedido;

namespace MVC_ComponentesCodeFirst.Services.Repositories
{
    public class PedidoRepositroy : IGenericRepository<Pedido>
    {
        private readonly OrdenadoresContext _context;
        private readonly FactoriaContextos _factoriaDeContextos = new();
        private readonly ILoggerManager _loggerManager;
        public PedidoRepositroy(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _context = _factoriaDeContextos.CreateDbContext(new string[1]);
        }

        public async Task<List<Pedido>> All()
        {
            return _context.Pedidos.Any()
                ? await _context.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Factura)
                    .Include(p => p.Ordenadores)!
                    .ThenInclude(o => o.Componentes)
                    .ToListAsync()
                : new List<Pedido>();
        }

        public async Task<Pedido?> GetById(int? id)
        {
            if (_context.Pedidos.Any() && id != null)
                return await _context.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Factura)
                    .Include(p => p.Ordenadores)!
                    .ThenInclude(o => o.Componentes)
                    .FirstOrDefaultAsync(m => m.Id == id);
            return null;
        }

        public SelectList ListaClientesId(int id = 0)
        {
            return id == 0 ? new SelectList(_context.Set<Cliente>(), "Id", "Apellido") : new SelectList(_context.Set<Cliente>(), "Id", "Apellido", id);
        }

        public SelectList ListaFacturasId(int id = 0)
        {
            return id == 0 ? new SelectList(_context.Facturas, "Id", "Descripcion") : new SelectList(_context.Facturas, "Id", "Descripcion", id);
        }
        public async Task Add(Pedido pedido)
        {
            try
            {
                _context.Pedidos.Add(pedido);
                _loggerManager.LogDebug(
                    $"Pedido añadido. Descripcion: {pedido.Descripcion}");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al añadir pedido. Message: " + ex.Message);
                throw;
            }
        }

        public async Task Edit(Pedido pedido)
        {
            try
            {
                _context.Update(pedido);
                _loggerManager.LogDebug(
                $"Pedido actualizado. Descripcion: {pedido.Descripcion}");

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al editar pedido. Message: " + ex.Message);
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var pedido = GetById(id);
                if (pedido.Result != null)
                {
                    _context.Pedidos.Remove(pedido.Result);
                    _loggerManager.LogDebug(
                        $"Pedido eliminado. Descripcion: {pedido.Result.Descripcion}");

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
                    var pedido = GetById(id);

                    if (pedido.Result == null) continue;
                    _context.Pedidos.Remove(pedido.Result);
                    _loggerManager.LogDebug(
                        $"Pedido eliminado.Descripcion: {pedido.Result.Descripcion}");
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar pedido. Message: " + ex.Message);
                throw;
            }
        }
    }
}
