using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.ViewComponents
{
    public class PedidoOrdenadorViewComponent : ViewComponent
    {
        private readonly IPedidoRepository _pedidoRepository;
        public PedidoOrdenadorViewComponent(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public Task<IViewComponentResult> InvokeAsync(int id)
        {
            var pedido = _pedidoRepository.GetById(id).Result;

            return Task.FromResult<IViewComponentResult>(View("Default", pedido));
        }
    }
}
