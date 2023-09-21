using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentesController : ControllerBase
    {
        private readonly IGenericRepositoryAdo<Componente> _componenteRepository;

        public ComponentesController(IGenericRepositoryAdo<Componente> repository)
        {
            _componenteRepository = repository;
        }

        // GET: api/Componentes
        [HttpGet]
        public ActionResult<IEnumerable<Componente>?> GetComponentes()
        {
            if (_componenteRepository.All() == null)
            {
                return NotFound();
            }

            return _componenteRepository.All();
        }

        // GET: api/Componentes/5
        [HttpGet("{id}")]
        public ActionResult<Componente> GetComponente(int id)
        {
            if (_componenteRepository.All() == null)
            {
                return NotFound();
            }

            var componente = _componenteRepository.GetById(id);

            if (componente == null)
            {
                return NotFound();
            }

            return componente;
        }

        // PUT: api/Componentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutComponente(int id, Componente componente)
        {
            if (id != componente.Id)
            {
                return BadRequest();
            }


            try
            {
                _componenteRepository.Edit(componente);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponenteExists(id))
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

        // POST: api/Componentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Componente> PostComponente(Componente componente)
        {
            if (_componenteRepository.All() == null)
            {
                return Problem("Entity set 'OrdenadoresContext.Componentes'  is null.");
            }
            _componenteRepository.Add(componente);

            return CreatedAtAction("GetComponente", new { id = componente.Id }, componente);
        }

        // DELETE: api/Componentes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteComponente(int id)
        {
            if (_componenteRepository.All() == null)
            {
                return NotFound();
            }
            _componenteRepository.Delete(id);

            return NoContent();
        }
        private bool ComponenteExists(int id)
        {
            return _componenteRepository.GetById(id) != null;
        }
    }
}
