﻿using AdminApp.Areas.Administrador.Models.ViewModels;
using AdminApp.Areas.Administrador.Services;
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
            var turno = Service.GetTurnoActual(1);
            TurnoViewModel1 vm = new();
            vm = await turno;

            return View("Index",vm);
        }

        [HttpGet]
        public async Task<IActionResult> AdelantarTurno()
        {
            var avanzar = Service.Avanzar(1);

            TurnoViewModel1 vm = new();
            vm = await avanzar;

            return View("Index", vm);
        }
    }
}
