using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace AdminApp.Areas.Operador.Controllers
{
    [Area("Operador")]
    public class TurnoController : Controller
    {
        Service1 Service;
        HubConnection hub;

        public TurnoController()
        {
            Service = new();
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //COLOCAR EL ID DE ACUERDO A LA CAJA
            var turno = Service.GetTurnoActual(2);
            TurnoViewModel1 vm = new();
            vm = await turno;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AdelantarTurno()
        {
            var turnoSig = Service.Avanzar(2);

            TurnoViewModel1 vm = new();
            vm = await turnoSig;

            return View("Index", vm);
        }

		[HttpGet]
		public async Task<IActionResult> AtrasarTurno()
        {
            var turnoAnterior = Service.Atrasar(2);

            TurnoViewModel1 vm = new();
            vm = await turnoAnterior;

            return View("Index", vm);
        }

        private async Task Iniciar()
        {
            hub = new HubConnectionBuilder()
              .WithUrl("https://localhost:44385/turnos")
              .WithAutomaticReconnect()
              .Build();

            await hub.StartAsync();
        }

        [HttpGet]
        public async Task<IActionResult> LlamarCliente(int id)
        {
            if(id != 0)
            {
                await Task.Run(() => Iniciar());
                await hub.InvokeAsync("LlamarCliente", id);
                TurnoViewModel1 vm = new();
                var clienteactualizado = await Service.GetTurno(id);
                vm = clienteactualizado;
                return View("Index", vm);   
            }
            return View("Index");
        }
	}
}
