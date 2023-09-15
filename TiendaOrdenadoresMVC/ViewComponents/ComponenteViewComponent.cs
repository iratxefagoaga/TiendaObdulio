using Microsoft.AspNetCore.Mvc;
using MVC_ComponentesCodeFirst.Services.Interfaces;

namespace MVC_ComponentesCodeFirst.ViewComponents
{
    public class ComponenteViewComponent : ViewComponent
    {
        private readonly IComponenteRepository _componenteRepository;
        public ComponenteViewComponent(IComponenteRepository componenteRepository)
        {
            _componenteRepository = componenteRepository;
        }
        public Task<IViewComponentResult> InvokeAsync(int id)
        {
            var componente = _componenteRepository.GetById(id).Result;

            return Task.FromResult<IViewComponentResult>(View("Default", componente));
        }
    }
}