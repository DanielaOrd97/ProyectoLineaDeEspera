using AdminApp.Models.Validators;
using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;

namespace AdminApp.Controllers
{
    public class AccountController : Controller
    {
        Service1 service;

        public AccountController()
        {
            service = new Service1();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            LogInViewModel vm = new();

            return View(vm);    
        }

        LogInValidator validator = new();

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel vm)
        {
            var resultado = validator.Validate(vm);

            if (!ModelState.IsValid) { 
                //vm.Error = string.Join("\n", resultado.Errors.Select(x => x.ErrorMessage));
                vm.Error = string.Join(Environment.NewLine, resultado.Errors.Select(x => x.ErrorMessage));
				return View(vm);
            }
           var r = await service.LogIn(vm);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(r) as JwtSecurityToken;

            //SACAR DE LAS CLAIMS EL ROL Y DE AHI DIRIGIR AL AREA CORRESPONDIENTE.
            var Id = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "CajaIdentifier")?.Value;
            var roleClaim = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;

            if(roleClaim == "Administrador")
            {
                return RedirectToAction("Index", "Home", new { area = "Administrador" });
            }
            else if(roleClaim == "Operador")
            {
                int idcaja = int.Parse(Id);

                return RedirectToAction("Index", "Turno", new { area = "Operador", idcaja});
            }

            return View(vm);
        }
    }
}
