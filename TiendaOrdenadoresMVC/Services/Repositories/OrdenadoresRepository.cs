using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Logging;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Services.Repositories
{
    public class OrdenadoresRepository : IGenericRepository<Ordenador>
    {
        private readonly OrdenadoresContext _context;
        private readonly FactoriaContextos _factoriaDeContextos = new();
        private readonly ILoggerManager _loggerManager;
        private readonly Converters _converter = new();

        public OrdenadoresRepository(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _context = _factoriaDeContextos.CreateDbContext(new string[1]);
        }

        public async Task Add(Ordenador ordenador)
        {
            try
            {
                //if (_converter.ConvertirOrdenador(ordenador) == null)
                //    throw new Exception("El ordenador no es valido");

                _context.Ordenadores.Add(ordenador);
                _loggerManager.LogDebug(
                    $"Ordenador añadido. Descripcion: {ordenador.Descripcion}");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al añadir ordenador. Message: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Ordenador>> All()
        {
            return _context.Ordenadores.Any()
                ? await _context.Ordenadores
                    .Include(o => o.Componentes)
                    .Include(o => o.Pedido).ToListAsync()
                : new List<Ordenador>();
        }


        public async Task Delete(int id)
        {
            try
            {
                var ordenador = GetById(id);
                if (ordenador.Result != null)
                {
                    _context.Ordenadores.Remove(ordenador.Result);
                    _loggerManager.LogDebug(
                        $"Ordenador eliminado. Descripcion: {ordenador.Result.Descripcion}");

                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar ordenador. Message: " + ex.Message);
                throw;
            }
        }

        public async Task Edit(Ordenador ordenador)
        {
            try
            {

                if (_converter.ConvertirOrdenador(ordenador) == null)
                    throw new Exception("El ordenador no es valido");
                _context.Update(ordenador);
                _loggerManager.LogDebug(
                    $"Ordenador actualizado. Descripcion: {ordenador.Descripcion}");

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al editar ordenador. Message: " + ex.Message);
                throw;
            }
        }

        public async Task<Ordenador?> GetById(int? id)
        {
            return _context.Ordenadores.Any()
                ? await _context.Ordenadores
                    .Include(o => o.Componentes)
                    .Include(o => o.Pedido)
                    .FirstOrDefaultAsync(m => m.Id == id)
                : null;

        }

        public SelectList PedidosLista(Ordenador? ordenador = null)
        {
            return ordenador == null ? new SelectList(_context.Pedidos, "Id", "Descripcion") : new SelectList(_context.Pedidos, "Id", "Descripcion", ordenador.PedidoId);
        }
        public async Task DeleteRange(int[] deleteInputs)
        {
            try
            {
                foreach (var id in deleteInputs)
                {
                    var ordenador = GetById(id);

                    if (ordenador.Result == null) continue;
                    _context.Ordenadores.Remove(ordenador.Result);
                    _loggerManager.LogDebug(
                        $"Ordenador eliminado. Descripcion: {ordenador.Result.Descripcion}");
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar ordenador. Message: " + ex.Message);
                throw;
            }
        }

    }
}
