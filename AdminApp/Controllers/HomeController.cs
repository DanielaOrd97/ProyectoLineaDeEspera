using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		public Service1 Service { get; }

		//Service1 Service;
		public List<TurnoViewModel1> listaTurnos { get; set; } = new();

        public HomeController(ILogger<HomeController> logger, Service1 service)
        {
            _logger = logger;
            //Service = new Service1();
            this.Service = service;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

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

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
