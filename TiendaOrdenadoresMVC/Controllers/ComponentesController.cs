using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Controllers
{
    public class ComponentesController : Controller
    {
        private readonly IComponenteRepository _repositorioComponente;
        public ComponentesController(IComponenteRepository repositorio)
        {
            _repositorioComponente = repositorio;
        }
        // GET: Componentes
        public async Task<IActionResult> Index()
        {
            return View("Index", await _repositorioComponente.All());
        }

        // GET: Componentes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View("Details", await _repositorioComponente.GetById(id));
        }

        // GET: Componentes/Create
        public  IActionResult Create()
        {
            ViewData["OrdenadorId"] = _repositorioComponente.OrdenadoresLista();
            return View("Create");
        }

        // POST: Componentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoComponente,Descripcion,Serie,Calor,Megas,Cores,Precio,OrdenadorId")] Componente componente)
        {
            try
            {
                await _repositorioComponente.Add(componente);
                ViewData["OrdenadorId"] = _repositorioComponente.OrdenadoresLista(componente.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Componentes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["OrdenadorId"] = _repositorioComponente.OrdenadoresLista(id);
            return View("Edit", await _repositorioComponente.GetById(id));
        }

        // POST: Componentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,TipoComponente,Descripcion,Serie,Calor,Megas,Cores,Precio,OrdenadorId")] Componente componente)
        {

            try
            {
                await _repositorioComponente.Edit(componente);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["OrdenadorId"] =  _repositorioComponente.OrdenadoresLista(componente.Id);
                return View("Edit", componente);
            }
        }

        // GET: Componentes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View("Delete", await _repositorioComponente.GetById(id));
        }

        // POST: Componentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                await _repositorioComponente.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Delete", await _repositorioComponente.GetById(id));
            }
        }

        public bool ComponenteExists(int id)
        {
            return _repositorioComponente.GetById(id).Result != null;
        }
        // POST: Componentes/BatchDelete/5
        [HttpPost, ActionName("BatchDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BatchDelete(int[] deleteInputs)
        {
            if (deleteInputs is not { Length: > 0 }) return RedirectToAction(nameof(Index));
            try
            {
                
                   await _repositorioComponente.DeleteRange(deleteInputs);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
