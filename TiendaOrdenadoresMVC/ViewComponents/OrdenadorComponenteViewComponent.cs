using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.ViewComponents
{

    public class OrdenadorComponenteViewComponent : ViewComponent
    {
        private readonly IOrdenadorRepository _ordenadorRepository;

        public OrdenadorComponenteViewComponent(IOrdenadorRepository ordenadorRepository)
        {
            _ordenadorRepository = ordenadorRepository;
        }

        public Task<IViewComponentResult> InvokeAsync(int id)
        {
            var ordenador = _ordenadorRepository.GetById(id).Result;

            return Task.FromResult<IViewComponentResult>(View("Default", ordenador));
        }
    }
}
