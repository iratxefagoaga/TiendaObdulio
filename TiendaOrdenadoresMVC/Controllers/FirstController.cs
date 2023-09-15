using Microsoft.AspNetCore.Mvc;

namespace MVC_ComponentesCodeFirst.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
