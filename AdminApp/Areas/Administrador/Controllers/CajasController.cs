using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Areas.Administrador.Controllers
{
    [Area("Administrador")]
	public class CajasController : Controller
	{
		Service1 Service;
		public List<CajaViewModel1> ListaCajas { get; set; } = new();

        public CajasController()
        {
            Service = new Service1();
        }
        public async Task<IActionResult> Index()
		{
			var cajas = await Service.GetCajas();

            foreach (var item in cajas)
            {
				ListaCajas.Add(item);
            }

            return View(ListaCajas);
		}

		[HttpGet]
		public IActionResult AgregarCaja()
		{
			CajaViewModel1 vm = new();
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> AgregarCaja(CajaViewModel1 vm)
		{
			if(vm != null)
			{
				await Service.AddCaja(vm);
				return RedirectToAction("Index");
			}
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> EditarCaja(int id)
		{
			var caja = await Service.GetCaja(id);

			if (caja != null)
			{
				CajaViewModel1 vm = new();
				vm = caja;
				return View(vm);
			}

			return null;
		}

		[HttpPost]
		public async Task<IActionResult> EditarCaja(CajaViewModel1 vm)
		{
			if(vm != null)
			{
				var caja = await Service.GetCaja(vm.Id);

				if(caja != null)
				{
					caja.NombreCaja = vm.NombreCaja;
					caja.Estado = vm.Estado;

					await Service.UpdateCaja(caja);
                    return RedirectToAction("Index");

                }
            }
			return View(vm);
		}

		/// <summary>
		/// ELIMINAR CAJAS.
		/// </summary>

        [HttpGet]
        public async Task<IActionResult> EliminarCaja(int id)
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

        [HttpPost]
        public async Task<IActionResult> EliminarCaja(CajaViewModel1 vm)
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
    }
}
