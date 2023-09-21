using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Models;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.ViewComponents
{
    public class ClientePedidoViewComponent :ViewComponent 
    {
        private readonly IGenericRepository<Cliente> _clienteRepository;
        public ClientePedidoViewComponent(IGenericRepository<Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public Task<IViewComponentResult> InvokeAsync(int id)
        {
            var cliente = _clienteRepository.GetById(id);

            return Task.FromResult<IViewComponentResult>(View("Default", cliente.Result));
        }
    }
}
