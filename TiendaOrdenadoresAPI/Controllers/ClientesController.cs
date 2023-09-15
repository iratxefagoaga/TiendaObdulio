using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository repository)
        {
            _clienteRepository = repository;
        }

        // GET: api/Clientes
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>?> GetClientes()
        {
          if (_clienteRepository.All() == null)
          {
              return NotFound();
          }

          return _clienteRepository.All();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(int id)
        {
          if (_clienteRepository.All() == null)
          {
              return NotFound();
          }

          var cliente = _clienteRepository.GetById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }
            

            try
            {
                 _clienteRepository.Edit(cliente);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Cliente> PostCliente(Cliente cliente)
        {
          if (_clienteRepository.All() == null)
          {
              return Problem("Entity set 'OrdenadoresContext.Clientes'  is null.");
          }
          _clienteRepository.Add(cliente);

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            if (_clienteRepository.All() == null)
            {
                return NotFound();
            }
            _clienteRepository.Delete(id);

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _clienteRepository.GetById(id) != null;
        }
    }
}
