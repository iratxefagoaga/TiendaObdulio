
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Logging;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Services.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {

        private readonly OrdenadoresContext _context;
        private readonly FactoriaContextos _factoriaDeContextos = new();
        private readonly ILoggerManager _loggerManager;
        private DbSet<T> _table;
        private DbSet<Ordenador> _ordenadores;
        private DbSet<Pedido> _pedidos;
        private DbSet<Cliente> _clientes;
        private DbSet<Factura> _facturas;
        public GenericRepository(ILoggerManager loggerManager)
        {

            _loggerManager = loggerManager;
            _context = _factoriaDeContextos.CreateDbContext(new string[1]);
            _table = _context.Set<T>();
            _ordenadores = _context.Set<Ordenador>();
            _pedidos = _context.Set<Pedido>();
            _clientes = _context.Set<Cliente>();
            _facturas = _context.Set<Factura>();
        }

        public Task Add(T obj)
        {
            try
            {
                _table.Add(obj);
                _loggerManager.LogDebug(
                    $"Objeto añadido correctamente");
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al añadir objeto. Message: " + ex.Message);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task<List<T>> All()
        {
            return _table.ToListAsync();
        }

        public Task Delete(int id)
        {
            try
            {
                var obj = GetById(id);
                if (obj.Result != null)
                {
                    _context.Remove(obj.Result);
                    _loggerManager.LogDebug(
                        $"Objeto eliminado");

                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar objeto. Message: " + ex.Message);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task DeleteRange(int[] input)
        {
            try
            {
                foreach (var id in input)
                {
                    var obj = GetById(id);

                    if (obj.Result == null) continue;
                    _table.Remove(obj.Result);
                    _loggerManager.LogDebug(
                        $"Objeto eliminado");
                }
                
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar objeto");
                throw;
            }

            return Task.CompletedTask;
        }

        public Task Edit(T obj)
        {
            try
            {
                _table.Update(obj);
                _loggerManager.LogDebug(
                    $"Objeto actualizado");
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al actualizar objeto. Message: " + ex.Message);
                throw;
            }

            return Task.CompletedTask;
        }
        public async Task<T?> GetById(int? id)
        {
            return _context.Clientes.Any()
                ? await _table.FirstOrDefaultAsync(m => m.Id == id)
                : null;
        }

        public SelectList OrdenadoresLista(int ordenadorId = 0)
        {
            return ordenadorId == 0 ? new SelectList(_ordenadores, "Id", "Descripcion") : new SelectList(_ordenadores, "Id", "Descripcion", ordenadorId);
        }

        public SelectList PedidosLista(Ordenador? ordenador = null)
        {
            return ordenador == null ? new SelectList(_pedidos, "Id", "Descripcion") : new SelectList(_pedidos, "Id", "Descripcion", ordenador.PedidoId);
        }
        public SelectList ListaClientesId(int id = 0)
        {
            return id == 0 ? new SelectList(_clientes, "Id", "Apellido") : new SelectList(_clientes, "Id", "Apellido", id);
        }

        public SelectList ListaFacturasId(int id = 0)
        {
            return id == 0 ? new SelectList(_facturas, "Id", "Descripcion") : new SelectList(_facturas, "Id", "Descripcion", id);
        }
    }
}
