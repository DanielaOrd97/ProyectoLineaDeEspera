using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
