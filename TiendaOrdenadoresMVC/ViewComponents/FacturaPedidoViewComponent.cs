using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.ViewComponents
{
    public class FacturaPedidoViewComponent : ViewComponent
    {
        private readonly IFacturasRepository _facturasRepository;
        public FacturaPedidoViewComponent(IFacturasRepository facturasRepository)
        {
            _facturasRepository = facturasRepository;
        }
        public Task<IViewComponentResult> InvokeAsync(int id)
        {
            var factura = _facturasRepository.GetById(id).Result;

            return Task.FromResult<IViewComponentResult>(View("Default", factura));
        }
    }
}
