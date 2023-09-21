using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IGenericRepository<Cliente> _repositoryCliente;

        public ClientesController(IGenericRepository<Cliente> repositorio)
        {
            _repositoryCliente = repositorio;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View("Index", await _repositoryCliente.All());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View("Details", await _repositoryCliente.GetById(id));
        }

        // GET: Clientes/Create
        public  IActionResult Create()
        {
            return  View("Create");
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Password,CreditCard")] Cliente cliente)
        {
            try
            {
                await _repositoryCliente.Add(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View("Edit", await _repositoryCliente.GetById(id));
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Nombre,Apellido,Email,Password,CreditCard")] Cliente cliente)
        {
            try
            {
                await _repositoryCliente.Edit(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit", cliente);
            }
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View("Delete", await _repositoryCliente.GetById(id));
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                await _repositoryCliente.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Delete", await _repositoryCliente.GetById(id));
            }
        }

        // POST: Clientes/BatchDelete/5
        [HttpPost, ActionName("BatchDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BatchDelete(int[] deleteInputs)
        {
            if (deleteInputs is not { Length: > 0 }) return RedirectToAction(nameof(Index));
            try
            {
                await _repositoryCliente.DeleteRange(deleteInputs);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public bool ClienteExists(int id)
        {
            return _repositoryCliente.GetById(id).Result != null;
        }
    }
}