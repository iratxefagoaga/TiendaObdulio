using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenadoresController : ControllerBase
    {
        private readonly IGenericRepositoryAdo<Ordenador> _ordenadorRepository;

        public OrdenadoresController(IGenericRepositoryAdo<Ordenador> repository)
        {
            _ordenadorRepository = repository;
        }

        // GET: api/Ordenadores
        [HttpGet]
        public ActionResult<IEnumerable<Ordenador>?> GetOrdenadores()
        {
            if (_ordenadorRepository.All() == null)
            {
                return NotFound();
            }

            return _ordenadorRepository.All();
        }

        // GET: api/Ordenadores/5
        [HttpGet("{id}")]
        public ActionResult<Ordenador> GetOrdenador(int id)
        {
            if (_ordenadorRepository.All() == null)
            {
                return NotFound();
            }

            var ordenador = _ordenadorRepository.GetById(id);

            if (ordenador == null)
            {
                return NotFound();
            }

            return ordenador;
        }

        // PUT: api/Ordenadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutOrdenador(int id, Ordenador ordenador)
        {
            if (id != ordenador.Id)
            {
                return BadRequest();
            }


            try
            {
                _ordenadorRepository.Edit(ordenador);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenadorExists(id))
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

        // POST: api/Ordenadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Ordenador> PostOrdenador(Ordenador ordenador)
        {
            if (_ordenadorRepository.All() == null)
            {
                return Problem("Entity set 'OrdenadoresContext.Ordenadores'  is null.");
            }
            _ordenadorRepository.Add(ordenador);

            return CreatedAtAction("GetOrdenador", new { id = ordenador.Id }, ordenador);
        }

        // DELETE: api/Ordenadores/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrdenador(int id)
        {
            if (_ordenadorRepository.All() == null)
            {
                return NotFound();
            }
            _ordenadorRepository.Delete(id);

            return NoContent();
        }

        private bool OrdenadorExists(int id)
        {
            return _ordenadorRepository.GetById(id) != null;
        }
    }
}
