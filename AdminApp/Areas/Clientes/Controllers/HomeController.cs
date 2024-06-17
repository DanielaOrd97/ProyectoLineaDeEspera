using AdminApp.Models.DTOs;
using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminApp.Areas.Clientes.Controllers
{
    [Area("Clientes")]
    public class HomeController : Controller
    {
        Service1 Service;
        HubConnection hub;
        public List<TurnoViewModel1> listaTurnos { get; set; } = new();

        public HomeController()
        {
            Service = new Service1();
            //Task.Run(() => Iniciar());
        }

        //private async Task Iniciar()
        //{
        //    hub = new HubConnectionBuilder()
        //      .WithUrl("https://localhost:44385/turnos")
        //      .WithAutomaticReconnect()
        //    .Build();

        //    await hub.StartAsync();

            
            
            
        //    hub.On<TurnoViewModel1>("AbandonarTurno", x =>
        //    {
        //        var turnoAbandonado = listaTurnos.FirstOrDefault(a => a == x);

        //        if(turnoAbandonado != null)
        //        {
        //            listaTurnos.Remove(turnoAbandonado);
        //        }
        //    });
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
    }
}
