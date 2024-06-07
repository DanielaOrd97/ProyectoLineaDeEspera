﻿using AdminApp.Areas.Administrador.Models.ViewModels;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;

namespace AdminApp.Areas.Administrador.Services
{
	public class Service1
	{
		HttpClient client;

        public Service1()
        {
			client = new()
			{
				BaseAddress = new Uri("https://localhost:44385/api/")
				//BaseAddress = new Uri("https://apilineaesperaeq2.websitos256.com/api")
			};
		}

		/// <summary>
		/// VER TODA LA INFORMACION DE LOS USUARIOS REGISTRADOS.
		/// </summary>
		public async Task<List<UsuarioViewModel1>> GetAllUsuarios()
		{
			List<UsuarioViewModel1> usuarioslist = new();

			var response = await client.GetAsync($"Usuarios");

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				//string data = response.Content.ReadAsStringAsync().Result;
				usuarioslist = JsonConvert.DeserializeObject<List<UsuarioViewModel1>>(jsonResponse);
			}

			return usuarioslist;
		}

		///<summary>
		///VER INFORMACION DE SOLO ADMIN.
		/// </summary>
		/// 
		public async Task<List<UsuarioViewModel1>> GetAdmins()
		{
			List<UsuarioViewModel1> adminlist = new();
			var response = await client.GetAsync($"Usuarios/Admin");

			if (response.IsSuccessStatusCode)
			{
				var jsonresult = await response.Content.ReadAsStringAsync();
				adminlist = JsonConvert.DeserializeObject<List<UsuarioViewModel1>>(jsonresult);
			}

			return adminlist;
		}

		///<summary>
		///VER INFORMACION DE SOLO OPERADORES.
		/// </summary>
		public async Task<List<UsuarioViewModel1>> GetOperadores()
		{
			List<UsuarioViewModel1> operadoreslist = new();
			var response = await client.GetAsync($"Usuarios/Operadores");

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				//string data = response.Content.ReadAsStringAsync().Result;
				operadoreslist = JsonConvert.DeserializeObject<List<UsuarioViewModel1>>(jsonResponse);
			}
			return operadoreslist;
		}

		///<summary>
		///VER INFORMACION DE SOLO CLIENTES
		/// </summary>
		/// 
		public async Task<List<UsuarioViewModel1>> GetClientes()
		{
			List<UsuarioViewModel1> clienteslist = new();
			var response = await client.GetAsync($"Usuarios/Clientes");

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				clienteslist = JsonConvert.DeserializeObject<List<UsuarioViewModel1>>(jsonResponse);
			}

			return clienteslist;
		}

		///<summary>
		///VER TURNOS POR CAJA.
		/// </summary>
		/// 
		public async Task<List<List<TurnoViewModel1>>> GetTurnosPorCaja()
		{
			List<List<TurnoViewModel1>> turnoslist = new();
			var response = await client.GetAsync($"Turnos/TurnosPorCaja");

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				turnoslist = JsonConvert.DeserializeObject<List<List<TurnoViewModel1>>>(jsonResponse);
			}

			return turnoslist;
		}

		///<summary>
		///CARGAR TODOS LOS ROLES.
		/// </summary>
		/// 
		public async Task<List<RolViewModel1>> GetAllRoles()
		{
			List<RolViewModel1> listRoles = new();

			var response = await client.GetAsync($"Roles");

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				listRoles = JsonConvert.DeserializeObject<List<RolViewModel1>>(jsonResponse);
			}

			return listRoles;
		}

		///<summary>
		///CARGAR TODAS LAS CAJAS
		/// </summary>
		/// 
		public async Task<List<CajaViewModel1>> GetAllCajas()
		{
			List<CajaViewModel1> listCajas = new();

			var response = await client.GetAsync($"Cajas");

			if (response.IsSuccessStatusCode)
			{
				var jsonresponse = await response.Content.ReadAsStringAsync();
				listCajas = JsonConvert.DeserializeObject<List<CajaViewModel1>>(jsonresponse);
			}

			return listCajas;
		}

		///<summary>
		///CRUD USUARIOS
		/// </summary>
		/// 

		public async Task AddUsuario(AgregarUsuarioViewModel1 dto)
		{
			var response = await client.PostAsJsonAsync($"Usuarios/AgregarUsuario", dto);

			if (response.IsSuccessStatusCode)
			{

			}
		}

		public async Task UpdateUsuario(AgregarUsuarioViewModel1 dto)
		{
			var response = await client.PutAsJsonAsync($"Usuarios/{dto.Id}", dto);

			if (response.IsSuccessStatusCode)
			{

			}
		}

		public async Task DeleteUsuario(int id)
		{
			var response = await client.DeleteAsync($"Usuarios/{id}");

			if (response.IsSuccessStatusCode)
			{

			}
		}

		public async Task<AgregarUsuarioViewModel1> GetUsuario(int id)
		{
			var response = await client.GetAsync($"Usuarios/{id}");

			if (response.IsSuccessStatusCode)
			{
				var jsonresponse = await response.Content.ReadAsStringAsync();
				var user = JsonConvert.DeserializeObject<AgregarUsuarioViewModel1>(jsonresponse);
				return user;
			}
			return null;
		}

		public async Task<UsuarioViewModel1> GetUsuario1(int id)
		{
			var response = await client.GetAsync($"Usuarios/{id}");

			if (response.IsSuccessStatusCode)
			{
				var jsonresponse = await response.Content.ReadAsStringAsync();
				var user = JsonConvert.DeserializeObject<UsuarioViewModel1>(jsonresponse);
				return user;
			}
			return null;
		}
	}
}
