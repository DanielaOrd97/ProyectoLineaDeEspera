using AdminApp.Areas.Administrador.Models.ViewModels;
using AdminApp.Areas.Administrador.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Areas.Administrador.Controllers
{
    [Area("Administrador")]
	public class TurnosController : Controller
	{
		Service1 service;
		public List<List<TurnoViewModel1>> listaTurnos { get; set; } = new();

        public TurnosController()
        {
            service = new Service1();
        }

        public async Task<IActionResult> Index()
        {
			var response = await service.GetTurnosPorCaja();

            foreach (var item in response)
            {
                listaTurnos.Add(item);
            }

            return View(listaTurnos);
		}


	}
}
