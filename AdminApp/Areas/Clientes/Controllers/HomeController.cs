using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Areas.Clientes.Controllers
{
    [Area("Clientes")]
    public class HomeController : Controller
    {
        Service1 Service;
        public List<TurnoViewModel1> listaTurnos { get; set; } = new();

        public HomeController()
        {
            Service = new Service1();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await Service.GetAllTurnos();

            foreach (var item in result)
            {
                listaTurnos.Add(item);

            }

            ClienteViewModel vm = new();
            vm.ListaTurnos = listaTurnos;

            return View(vm);
        }
    }
}
