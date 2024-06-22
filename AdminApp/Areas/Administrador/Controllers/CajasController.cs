using AdminApp.Areas.Administrador.Models.Validators;
using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Areas.Administrador.Controllers
{
    [Area("Administrador")]
	public class CajasController : Controller
	{
		//Service1 Service;
		public List<CajaViewModel1> ListaCajas { get; set; } = new();
		public Service1 Service { get; }

		public CajasController(Service1 service)
        {
            //Service = new Service1();
			this.Service = service;
        }
        public async Task<IActionResult> Index()
		{
			try
			{
                var cajas = await Service.GetCajas();

				if (cajas != null)
				{
                    foreach (var item in cajas)
					{
						ListaCajas.Add(item);
					}

                    return View(ListaCajas);
                }
            }
			catch (UnauthorizedAccessException ex)
			{
                return RedirectToAction("LogIn", "Account", new { area = "" });
            }
			return View();

			//var cajas = await Service.GetCajas();

   //         foreach (var item in cajas)
   //         {
			//	ListaCajas.Add(item);
   //         }

   //         return View(ListaCajas);
		}

        [HttpGet]
		public IActionResult AgregarCaja()
		{
			try
			{
                CajaViewModel1 vm = new();
                return View(vm);
            }
			catch (Exception)
			{
                return RedirectToAction("LogIn", "Account", new { area = "" });
            }

		}

		CajaAdminValidator validator = new();

		[HttpPost]
		public async Task<IActionResult> AgregarCaja(CajaViewModel1 vm)
		{
			try
			{

                var resultado = validator.Validate(vm);
                if (!ModelState.IsValid)
                {
                    vm.Error = string.Join(Environment.NewLine, resultado.Errors.Select(x => x.ErrorMessage));
                    return View(vm);
                }
                if (vm != null)
                {
                    await Service.AddCaja(vm);
                    return RedirectToAction("Index");
                }
                return View(vm);
            }
			catch (Exception)
			{
                return RedirectToAction("LogIn", "Account", new { area = "" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditarCaja(int id)
		{
			try
			{
                var caja = await Service.GetCaja(id);

                if (caja != null)
                {
                    CajaViewModel1 vm = new();
                    vm = caja;
                    return View(vm);
                }

                return RedirectToAction("Index");
            }
			catch (Exception)
			{
                return RedirectToAction("LogIn", "Account", new { area = "" });
            }
			
		}

		[HttpPost]
		public async Task<IActionResult> EditarCaja(CajaViewModel1 vm)
		{

            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                if (vm != null)
                {
                    var caja = await Service.GetCaja(vm.Id);

                    if (caja != null)
                    {
                        caja.NombreCaja = vm.NombreCaja;
                        caja.Estado = vm.Estado;

                        await Service.UpdateCaja(caja);
                        return RedirectToAction("Index");

                    }
                }
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("LogIn", "Account", new { area = "" });
            }

		}

		/// <summary>
		/// ELIMINAR CAJAS.
		/// </summary>

        [HttpGet]
        public async Task<IActionResult> EliminarCaja(int id)
        {
            try
            {
                var caja = await Service.GetCaja(id);

                if (caja == null)
                {
                    return RedirectToAction("Index");
                }
                CajaViewModel1 vm = new();
                vm = caja;

                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("LogIn", "Account", new { area = "" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> EliminarCaja(CajaViewModel1 vm)
        {
            try
            {
                if (vm != null)
                {
                    var caja = await Service.GetCaja(vm.Id);

                    if (caja != null)
                    {
                        await Service.DeleteCaja(caja.Id);
                        return RedirectToAction("Index");
                    }
                }
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("LogIn", "Account", new { area = "" });
            }


           
        }
    }
}
