using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class HomeController : Controller
    {
        //Service1 service;

        public HomeController(Service1 service)
        {
            // service = new Service1();
            this.service = service;
        }

		public Service1 service { get; }

		public async Task<IActionResult> Index()
        {
            var cajasActivas = await service.GetCajasActivas();
            var cajasInactivas = await service.GetCajasInactivas();
            var turnosEnEspera = await service.GetTotalTurnosEspera();
            var turnosAtendidos = await service.GetTotalTurnosAtendidos();  

            EstadisticasViewModel vm = new();
            vm.TotalCajasActivas = cajasActivas;
            vm.TotalCajasInactivas = cajasInactivas;
            vm.TotalClientesAtendidos = turnosAtendidos;
            vm.TotalClientesEnEspera = turnosEnEspera;

            return View(vm);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
			return RedirectToAction("LogIn", "Account", new { area = "" });
		}
    }
}
