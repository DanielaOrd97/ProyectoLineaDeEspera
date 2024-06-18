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
        public static int IdCaja { get; set; }

        public TurnoController()
        {
            Service = new();
        }


        [HttpGet]
        public async Task<IActionResult> Index(int idcaja)
       {
            //COLOCAR EL ID DE ACUERDO A LA CAJA
            IdCaja = idcaja;
            var turno = Service.GetTurnoActual(idcaja);
            TurnoViewModel1 vm = new();
            
            
            vm = await turno;

            return View(vm);

        }

        //[HttpGet]
        //public async Task<IActionResult> AvanzarTurno()
        //{
        //    var turnoSig = Service.Avanzar(1);

        //    TurnoViewModel1 vm = new();
        //    vm = await turnoSig;

        //    return View("Index", vm);
        //}

        [HttpGet]
        public async Task<IActionResult> AvanzarTurno()
        {
            var turnoSig = Service.Avanzar(IdCaja);

            TurnoViewModel1 vm = new();
            vm = await turnoSig;

            return View("Index", vm);
        }

        [HttpGet]
		public async Task<IActionResult> AtrasarTurno()
        {
            var turnoAnterior = Service.Atrasar(IdCaja);

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

        [HttpGet]
        public async Task<IActionResult> AtenderCliente(int id)
        {
            await Task.Run(() => Iniciar());
            await hub.InvokeAsync("AtenderCliente", id);
            TurnoViewModel1 vm = new();
            var clienteactualizado = await Service.GetTurno(id);
            vm = clienteactualizado;
            return View("Index", vm);
        }

        [HttpGet]
        public async Task<IActionResult> TerminarAtencion(int id)
        {
            await Task.Run(() => Iniciar());
            await hub.InvokeAsync("TerminarAtencion", id);
            TurnoViewModel1 vm = new();
            var clienteactualizado = await Service.GetTurno(id);
            vm = clienteactualizado;

            return View("Index", vm);
        }

    }
}
