using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOrdenadoresAPI.Models;
using TiendaOrdenadoresAPI.Services.Interfaces;

namespace TiendaOrdenadoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IGenericRepositoryAdo<Pedido> _pedidoRepository;

        public PedidosController(IGenericRepositoryAdo<Pedido> repository)
        {
            _pedidoRepository = repository;
        }

        // GET: api/Pedidos
        [HttpGet]
        public ActionResult<IEnumerable<Pedido>?> GetPedidos()
        {
            if (_pedidoRepository.All() == null)
            {
                return NotFound();
            }

            return _pedidoRepository.All();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public ActionResult<Pedido> GetPedido(int id)
        {
            if (_pedidoRepository.All() == null)
            {
                return NotFound();
            }

            var pedido = _pedidoRepository.GetById(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }


            try
            {
                _pedidoRepository.Edit(pedido);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Pedido> PostPedido(Pedido pedido)
        {
            if (_pedidoRepository.All() == null)
            {
                return Problem("Entity set 'OrdenadoresContext.Pedidos'  is null.");
            }
            _pedidoRepository.Add(pedido);

            return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public IActionResult DeletePedido(int id)
        {
            if (_pedidoRepository.All() == null)
            {
                return NotFound();
            }
            _pedidoRepository.Delete(id);

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _pedidoRepository.GetById(id) != null;
        }
    }
}
