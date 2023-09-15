using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Services.Interfaces;
using Factura = MVC_ComponentesCodeFirst.Models.Factura;

namespace MVC_ComponentesCodeFirst.Controllers
{
    public class FacturasController : Controller
    {

        private readonly IFacturasRepository _repositorioFacturas;
        public FacturasController(IFacturasRepository repositorio)
        {
            _repositorioFacturas = repositorio;
        }
        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            return View("Index", await _repositorioFacturas.All());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View("Details", await _repositorioFacturas.GetById(id));
        }

        // GET: Facturas/Create
        public  IActionResult Create()
        {
            return View("Create");
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Factura factura)
        {
            if (!ModelState.IsValid) return View("Create");
            try
            {
                factura.Fecha = DateTime.UtcNow;
                await _repositorioFacturas.Add(factura);
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View("Create");
            }
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var factura = await _repositorioFacturas.GetById(id);
            return View("Edit", factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Factura factura)
        {
            try
            {
                await _repositorioFacturas.Edit(factura);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit",factura);
            }
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View("Delete", await _repositorioFacturas.GetById(id));
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                await _repositorioFacturas.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Delete",await _repositorioFacturas.GetById(id));
            }
        }
        // POST: Facturas/BatchDelete/5
        [HttpPost, ActionName("BatchDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BatchDelete(int[] deleteInputs)
        {
            if (deleteInputs is not { Length: > 0 }) return RedirectToAction(nameof(Index));
            try
            {

                await _repositorioFacturas.DeleteRange(deleteInputs);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public bool FacturaExists(int id)
        {
            return _repositorioFacturas.GetById(id).Result != null;
        }
    }
}
