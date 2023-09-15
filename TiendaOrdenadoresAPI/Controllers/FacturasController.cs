using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturasRepository _facturasRepository;

        public FacturasController(IFacturasRepository repository)
        {
            _facturasRepository = repository;
        }

        // GET: api/Facturas
        [HttpGet]
        public ActionResult<IEnumerable<Factura>?> GetFacturas()
        {
            if (_facturasRepository.All() == null)
            {
                return NotFound();
            }

            return _facturasRepository.All();
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public ActionResult<Factura> GetFactura(int id)
        {
            if (_facturasRepository.All() == null)
            {
                return NotFound();
            }

            var factura = _facturasRepository.GetById(id);

            if (factura == null)
            {
                return NotFound();
            }

            return factura;
        }

        // PUT: api/Facturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutFactura(int id, Factura factura)
        {
            if (id != factura.Id)
            {
                return BadRequest();
            }


            try
            {
                _facturasRepository.Edit(factura);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Facturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Factura> PostFactura(Factura factura)
        {
            if (_facturasRepository.All() == null)
            {
                return Problem("Entity set 'OrdenadoresContext.Facturas'  is null.");
            }
            _facturasRepository.Add(factura);

            return CreatedAtAction("GetFactura", new { id = factura.Id }, factura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFactura(int id)
        {
            if (_facturasRepository.All() == null)
            {
                return NotFound();
            }
            _facturasRepository.Delete(id);

            return NoContent();
        }

        private bool FacturaExists(int id)
        {
            return _facturasRepository.GetById(id) != null;
        }
    }
}
