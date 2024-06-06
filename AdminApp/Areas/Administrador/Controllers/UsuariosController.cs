﻿using AdminApp.Areas.Administrador.Models.ViewModels;
using AdminApp.Areas.Administrador.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Areas.Administrador.Controllers
{
	[Area("Administrador")]
	public class UsuariosController : Controller
	{
		Service1 Service;
		public List<UsuarioViewModel1> listaUsuarios { get; set; } = new();
        public UsuariosController()
        {
			Service = new Service1();
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var result = await Service.GetAllUsuarios();

			foreach (var item in result)
			{
				listaUsuarios.Add(item);
			}

			return View("Index", listaUsuarios);
		}

		[HttpGet]
		public async Task<IActionResult> Administradores()
		{
			listaUsuarios.Clear();

			var result = await Service.GetAdmins();

			foreach (var item in result)
			{
				listaUsuarios.Add(item);
			}

			return View("Index", listaUsuarios);
		}

		[HttpGet]
		public async Task<IActionResult> Operadores()
		{
			listaUsuarios.Clear();

			var result = await Service.GetOperadores();

			foreach (var item in result)
			{
				listaUsuarios.Add(item);
			}

			return View("Index", listaUsuarios);
		}

		[HttpGet]
		public async Task<IActionResult> Clientes()
		{
			listaUsuarios.Clear();

			var result = await Service.GetClientes();

			foreach (var item in result)
			{
				listaUsuarios.Add(item);
			}

			return View("Index", listaUsuarios);
		}

		[HttpGet]
		public async Task<IActionResult> AgregarUsuario()
		{
			AgregarUsuarioViewModel1 vm = new();
			vm.ListaRoles = await Service.GetAllRoles();
			vm.ListaCajas = await Service.GetAllCajas();

			return View(vm);
		}

		[HttpPost]
		public async Task AgregarUsuario(AgregarUsuarioViewModel1 vm)
		{
			//Agregar validaciones correctamente.

			if (vm != null)
			{
				await Service.AddUsuario(vm);

				RedirectToAction("Index");
				//REGRESAR AL INDEX
			}
		}

		[HttpGet]
		public async Task<IActionResult> EditarUsuario(int id)
		{
			var usuario = await Service.GetUsuario(id);

			//VALIDAR QUE NO ESTE VACIO
			if (usuario == null)
			{
				return null;
				Index();
			}
			else
			{
				AgregarUsuarioViewModel1 vm = new();
				vm = usuario;
				vm.ListaRoles = await Service.GetAllRoles();
				vm.ListaCajas = await Service.GetAllCajas();
				return View(vm);
			}
		}

		[HttpPost]
		public async Task<IActionResult> EditarUsuario(AgregarUsuarioViewModel1 vm)
		{
			if (vm != null)
			{
				var usuario = await Service.GetUsuario((int)vm.Id);

				if (usuario == null)
				{
					RedirectToAction("Index");
				}

				usuario.NombreUsuario = vm.NombreUsuario;
				usuario.Contraseña = vm.Contraseña;
				usuario.Nombre = vm.Nombre;
				usuario.IdRol = vm.IdRol;
				usuario.IdCaja = vm.IdCaja;

				await Service.UpdateUsuario(usuario);
			}
			return RedirectToAction("Index");

		}


		///<summary>
		///BAJA FISICA.
		/// </summary>

		[HttpGet]
		public async Task<IActionResult> EliminarUsuario(int id)
		{
			var usuario = await Service.GetUsuario1(id);

			if (usuario == null)
			{
				return RedirectToAction("Index");
			}
			UsuarioViewModel1 vm = new();
			vm = usuario;

			return View(vm);
		}
	}

}