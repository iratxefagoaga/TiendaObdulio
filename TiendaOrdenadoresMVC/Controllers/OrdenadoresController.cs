using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Controllers
{
    public class OrdenadoresController : Controller
    {
        private readonly IOrdenadorRepository _repositorioOrdenador;
        public OrdenadoresController(IOrdenadorRepository repositorio)
        {
            _repositorioOrdenador = repositorio;
        }

        // GET: Ordenadores
        public async Task<IActionResult> Index()
        {
            return View("Index", await _repositorioOrdenador.All());
        }

        // GET: Ordenadores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View("Details", await _repositorioOrdenador.GetById(id));
        }

        // GET: Ordenadores/Create
        public IActionResult Create()
        {
            ViewData["PedidoId"] = _repositorioOrdenador.PedidosLista();
            return View("Create");
        }

        // POST: Ordenadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,PedidoId")] Ordenador ordenador)
        {
            try
            {
                await _repositorioOrdenador.Add(ordenador);
                ViewData["PedidoId"] = _repositorioOrdenador.PedidosLista(ordenador);
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                ViewData["PedidoId"] = _repositorioOrdenador.PedidosLista(ordenador);
                return View("Create", ordenador);
            }
           
        }

        // GET: Ordenadores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
           
            ViewData["PedidoId"] = _repositorioOrdenador.PedidosLista(_repositorioOrdenador.GetById(id).Result);
            return View("Edit", await _repositorioOrdenador.GetById(id));
        }

        // POST: Ordenadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,PedidoId")] Ordenador ordenador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repositorioOrdenador.Edit(ordenador);
                    ViewData["PedidoId"] = _repositorioOrdenador.PedidosLista(ordenador);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewData["PedidoId"] = _repositorioOrdenador.PedidosLista(ordenador);
                    return View("Edit", ordenador);
                }
            }
            ViewData["PedidoId"] = _repositorioOrdenador.PedidosLista(ordenador);
            return View("Edit", ordenador);
        }

        // GET: Ordenadores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View("Delete", await _repositorioOrdenador.GetById(id));
        }

        // POST: Ordenadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _repositorioOrdenador.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Delete" ,await _repositorioOrdenador.GetById(id));
            }
        }
        // POST: Ordenadores/BatchDelete/5
        [HttpPost, ActionName("BatchDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BatchDelete(int[]? deleteInputs)
        {
            if (deleteInputs is not { Length: > 0 }) return RedirectToAction("Index");
            try
            {
                 await _repositorioOrdenador.DeleteRange(deleteInputs);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public bool OrdenadorExists(int id)
        {
            return _repositorioOrdenador.GetById(id).Result != null;
        }
    }
}
