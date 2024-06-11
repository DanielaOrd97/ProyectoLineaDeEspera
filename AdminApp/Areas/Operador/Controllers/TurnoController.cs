using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Areas.Operador.Controllers
{
    [Area("Operador")]
    public class TurnoController : Controller
    {
        Service1 Service;

        public TurnoController()
        {
            Service = new();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //COLOCAR EL ID DE ACUERDO A LA CAJA
            var turno = Service.GetTurnoActual(1);
            TurnoViewModel1 vm = new();
            vm = await turno;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AdelantarTurno()
        {
            var turnoSig = Service.Avanzar(1);

            TurnoViewModel1 vm = new();
            vm = await turnoSig;

            return View("Index", vm);
        }

		[HttpGet]
		public async Task<IActionResult> AtrasarTurno()
        {
            var turnoAnterior = Service.Atrasar(1);

            TurnoViewModel1 vm = new();
            vm = await turnoAnterior;

            return View("Index", vm);
        }
	}
}
