using AdminApp.Models.ViewModels;
using AdminApp.Services;
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

        [HttpGet]
        public IActionResult GenerarTurno()
        {
            GenerarNumTicket(1);
            return View();
        }

        private string GenerarNumTicket(int id)
        {
            Random r = new Random();
            var abcedeario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int posicion = r.Next(1,26);
            var letra1 = abcedeario[posicion].ToString();
            posicion = r.Next(1, 26);
            var letra2 = abcedeario[posicion].ToString();
            string num = "";

            if(id < 10)
            {
                 num = (letra1 + letra2+ "00" + id).ToString();
            }
            else if(id >= 10 && id < 100)
            {
                 num = (letra1 + letra2 + "0" + id).ToString();
            }
            else
            {
                 num = (letra1 + letra2 + id).ToString();
            }


            return num;
        }
	}
}
