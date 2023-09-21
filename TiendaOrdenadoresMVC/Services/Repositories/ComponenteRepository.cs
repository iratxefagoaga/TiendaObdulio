using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Logging;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Componente = MVC_ComponentesCodeFirst.Models.Componente;

namespace MVC_ComponentesCodeFirst.Services.Repositories
{
    public class ComponenteRepository : IGenericRepository<Componente>
    {
        private readonly OrdenadoresContext _context;
        private readonly FactoriaContextos _factoriaDeContextos = new();
        private readonly ILoggerManager _loggerManager;
        private readonly Converters _converter = new();
        public ComponenteRepository(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _context = _factoriaDeContextos.CreateDbContext(new string[1]);
        }
        public async Task Add(Componente componente)
        {
            try
            {
                if (_converter.ConvertirComponente(componente) == null)
                    throw new Exception("El componente no es valido");

                _context.Componentes.Add(componente);
                _loggerManager.LogDebug(
                    $"Componente añadido. Serie: {componente.Serie}, descripcion: {componente.Descripcion}");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al añadir componente. Message: " + ex.Message);
                throw;
            }
        }

        public async Task Edit(Componente componente)
        {
            try
            {
                if (_converter.ConvertirComponente(componente) == null)
                    throw new Exception("El componente no es valido");
                _context.Update(componente);
                _loggerManager.LogDebug(
                    $"Componente actualizado. Serie: {componente.Serie}, descripcion: {componente.Descripcion}");

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al editar componente. Message: " + ex.Message);
                throw;
            }
        }


        public async Task Delete(int id)
        {
            try
            {
                var componente = GetById(id);
                if (componente.Result != null)
                {
                    _context.Componentes.Remove(componente.Result);
                    _loggerManager.LogDebug(
                        $"Componente eliminado. Serie: {componente.Result.Serie}, descripcion: {componente.Result.Descripcion}");

                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar componente. Message: " + ex.Message);
                throw;
            }
        }

        public Task<List<Componente>> All()
        {

            return Task.FromResult(_context.Componentes.Any() ? _context.Componentes.Include(c => c.Ordenador).ToList() : new List<Componente>());
        }
        public Task<Componente?> GetById(int? id)
        {
            var componente = _context.Componentes
                    .Include(c => c.Ordenador)
                    .FirstOrDefault(m => m.Id == id);

            if (componente == null) return Task.FromResult(componente);
            var ordenador1 = (from ordenador in _context.Ordenadores
                              where ordenador.Id == componente.OrdenadorId
                              select ordenador).First();

            componente.Ordenador = ordenador1;
            return Task.FromResult(componente)!;
        }

        public SelectList OrdenadoresLista(int ordenadorId = 0)
        {
            return ordenadorId == 0 ? new SelectList(_context.Ordenadores, "Id", "Descripcion") : new SelectList(_context.Ordenadores, "Id", "Descripcion", ordenadorId);
        }

        public async Task DeleteRange(int[] deleteInputs)
        {
            try
            {
                foreach (var id in deleteInputs)
                {
                    var componente = GetById(id);

                    if (componente.Result == null) continue;
                    _context.Componentes.Remove(componente.Result);
                    _loggerManager.LogDebug(
                        $"Componente eliminado. Serie: {componente.Result.Serie}, descripcion: {componente.Result.Descripcion}");
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError("Error al eliminar componente. Message: " + ex.Message);
                throw;
            }
        }
    }
}
