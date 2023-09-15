using Microsoft.AspNetCore.Mvc;

namespace MVC_ComponentesCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
