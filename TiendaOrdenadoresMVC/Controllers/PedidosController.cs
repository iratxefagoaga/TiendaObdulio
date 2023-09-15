using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IPedidoRepository _repositorioPedido;
        public PedidosController(IPedidoRepository repositorio)
        {
            _repositorioPedido = repositorio;
        }


        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            return View("Index", await _repositorioPedido.All());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View("Details", await _repositorioPedido.GetById(id));
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = _repositorioPedido.ListaClientesId();
            ViewData["FacturaId"] = _repositorioPedido.ListaFacturasId();
            return View("Create");
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    pedido.Fecha = DateTime.UtcNow;
                    await _repositorioPedido.Add(pedido);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewData["ClienteId"] = _repositorioPedido.ListaClientesId(pedido.ClienteId);
                    ViewData["FacturaId"] = _repositorioPedido.ListaFacturasId(pedido.FacturaId);
                    return View("Create", pedido);
                }
            }

            ViewData["ClienteId"] = _repositorioPedido.ListaClientesId(pedido.ClienteId);
            ViewData["FacturaId"] = _repositorioPedido.ListaFacturasId(pedido.FacturaId);
            return View("Create", pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var pedido = await _repositorioPedido.GetById(id);
            if (pedido == null) return View("Edit", pedido);
            ViewData["ClienteId"] = _repositorioPedido.ListaClientesId(pedido.ClienteId);
            ViewData["FacturaId"] = _repositorioPedido.ListaFacturasId(pedido.FacturaId);

            return View("Edit", pedido);
            
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pedido pedido)
        {
            try
            {
                await _repositorioPedido.Edit(pedido);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["ClienteId"] = _repositorioPedido.ListaClientesId(pedido.ClienteId);
                ViewData["PedidoId"] = _repositorioPedido.ListaFacturasId(pedido.FacturaId);
                return View("Edit", pedido);
            }
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View("Delete", await _repositorioPedido.GetById(id));
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

               await _repositorioPedido.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Delete" ,await _repositorioPedido.GetById(id));
            }
        }
        // POST: Pedidos/BatchDelete/5
        [HttpPost, ActionName("BatchDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BatchDelete(int[] deleteInputs)
        {
            if (deleteInputs is not { Length: > 0 }) return RedirectToAction(nameof(Index));
            try
            {

                await _repositorioPedido.DeleteRange(deleteInputs);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public bool PedidoExists(int id)
        {
            return _repositorioPedido.GetById(id).Result != null;
        }
    }
}
