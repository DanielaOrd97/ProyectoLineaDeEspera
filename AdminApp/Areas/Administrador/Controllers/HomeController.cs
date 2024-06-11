using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class HomeController : Controller
    {
        Service1 service;

        public HomeController()
        {
            service = new Service1();
        }
        public async Task<IActionResult> Index()
        {
            var cajasActivas = await service.GetCajasActivas();
            var cajasInactivas = await service.GetCajasInactivas();

            EstadisticasViewModel vm = new();
            vm.TotalCajasActivas = cajasActivas;
            vm.TotalCajasInactivas = cajasInactivas;

            return View(vm);
        }
    }
}
