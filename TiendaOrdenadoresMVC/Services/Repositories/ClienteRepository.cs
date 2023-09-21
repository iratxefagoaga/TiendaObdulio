using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Logging;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Services.Repositories
{
    public class ClienteRepository : IGenericRepository<Cliente>
    {
        private readonly OrdenadoresContext _context;
        private readonly FactoriaContextos _factoriaDeContextos = new();
        private readonly ILoggerManager _loggerManager;
        public ClienteRepository(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _context = _factoriaDeContextos.CreateDbContext(new string[1]);
        }

        public async Task Add(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                _loggerManager.LogDebug(
                    $"Cliente añadido. Nombre: {cliente.Nombre} Apellido: {cliente.Apellido}");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al añadir cliente. Message: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Cliente>> All()
        {
            return  await _context.Clientes.Include(o => o.Pedidos).ToListAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var cliente = GetById(id);
                if (cliente.Result != null)
                {
                    _context.Clientes.Remove(cliente.Result);
                    _loggerManager.LogDebug(
                        $"Cliente eliminado. Nombre: {cliente.Result.Nombre}");

                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar cliente. Message: " + ex.Message);
                throw;
            }
        }

        public async Task Edit(Cliente cliente)
        {
            try
            {
                _context.Update(cliente);
                _loggerManager.LogDebug(
                    $"Cliente actualizado. Nombre: {cliente.Nombre}");

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al actualizar componente. Message: " + ex.Message);
                throw;
            }
        }

        public async Task<Cliente?> GetById(int? id)
        {
            return _context.Clientes.Any()
                ?  await _context.Clientes
                    .Include(c => c.Pedidos)
                    .FirstOrDefaultAsync(m => m.Id == id)
                : null;
        }
        public async Task DeleteRange(int[] deleteInputs)
        {
            try
            {
                foreach (var id in deleteInputs)
                {
                    var cliente = GetById(id);

                    if (cliente.Result == null) continue;
                    _context.Clientes?.Remove(cliente.Result);
                    _loggerManager.LogDebug(
                        $"Ordenador eliminado. Nombre: {cliente.Result.Nombre}");
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar cliente. Message: " + ex.Message);
                throw;
            }
        }
    }
}
