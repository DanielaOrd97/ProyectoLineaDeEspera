using AdminApp.Models.DTOs;
using AdminApp.Models.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace AdminApp.Services
{
    public class Service1
    {
        HttpClient client;
        public static string? Token { get; set; }

        public Service1()
        {
            client = new()
            {
                BaseAddress = new Uri("https://localhost:44385/api/")
                //BaseAddress = new Uri("https://apilineaesperaeq2.websitos256.com/api/")
            };
        }

        ///<summary>
        ///LOG IN
        /// </summary>
        /// 
        public async Task<string> LogIn(LogInViewModel vm)
        {
            var response = await client.PostAsJsonAsync("Login", vm);

            try
            {
                var res = await response.Content.ReadAsStringAsync();
                //var r = JsonConvert.DeserializeObject<ResponseDTO>(res);

                Token = res;
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// VER TODA LA INFORMACION DE LOS USUARIOS REGISTRADOS.
        /// </summary>
        public async Task<List<UsuarioViewModel1>> GetAllUsuarios()
        {
            List<UsuarioViewModel1> usuarioslist = new();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var response = await client.GetAsync($"Usuarios");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                //string data = response.Content.ReadAsStringAsync().Result;
                usuarioslist = JsonConvert.DeserializeObject<List<UsuarioViewModel1>>(jsonResponse);
                return usuarioslist;
            }

            return null;
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
                return adminlist;
            }

            return null;
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
                return operadoreslist;
            }
            return null;
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
                return clienteslist;
            }

            return null;
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
                return turnoslist;
            }

            return null;
        }

        ///<summary>
        ///CARGAR TODOS LOS ROLES.
        /// </summary>
        /// 
        public async Task<List<RolViewModel1>> GetAllRoles()
        {
            List<RolViewModel1> listRoles = new();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

			var response = await client.GetAsync($"Roles");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                listRoles = JsonConvert.DeserializeObject<List<RolViewModel1>>(jsonResponse);
                return listRoles;
            }

            return null;
        }



        ///<summary>
        ///CRUD USUARIOS
        /// </summary>
        /// 

        public async Task AddUsuario(AgregarUsuarioViewModel1 dto)
        {
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

			var response = await client.PostAsJsonAsync($"Usuarios/AgregarUsuario", dto);

            if (response.IsSuccessStatusCode)
            {

            }
        }

        public async Task UpdateUsuario(AgregarUsuarioViewModel1 dto)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

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



        //////////////////////////////CAJAS/////////////////////////////////////
        ///

        public async Task<List<CajaViewModel1>> GetCajas()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var response = await client.GetAsync($"Cajas");

            if (response.IsSuccessStatusCode)
            {
                var jsonresponse = await response.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<CajaViewModel1>>(jsonresponse);
                return lista;
            }

            return null;
        }

        public async Task<CajaViewModel1> GetCaja(int id)
        {

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

			var response = await client.GetAsync($"Cajas/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonresponse = await response.Content.ReadAsStringAsync();
                var caja = JsonConvert.DeserializeObject<CajaViewModel1>(jsonresponse);
                return caja;
            }
            return null;
        }

        public async Task AddCaja(CajaViewModel1 dto)
        {
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

			var response = await client.PostAsJsonAsync($"Cajas", dto);

            if (response.IsSuccessStatusCode)
            {

            }
        }

        public async Task UpdateCaja(CajaViewModel1 dto)
        {
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

			var response = await client.PutAsJsonAsync($"Cajas", dto);

            if (response.IsSuccessStatusCode)
            {

            }
        }

        public async Task DeleteCaja(int id)
        {
            var response = await client.DeleteAsync($"Cajas/{id}");

            if (response.IsSuccessStatusCode)
            {

            }
        }

        ///<summary>
        ///USUARIO OPERADOR*****************************
        /// </summary>
        /// 


        ///<summary>
        ///VER TURNO DE ACUERDO A ID.
        /// </summary>
        /// 
        public async Task<TurnoViewModel1> GetTurno(int id)
        {
            var response = await client.GetAsync($"Turnos/Turno/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonresponse = await response.Content.ReadAsStringAsync();
                var turno = JsonConvert.DeserializeObject<TurnoViewModel1>(jsonresponse);
                return turno;
            }
            return null;
        }



        ///<summary>
        ///VER TURNO ACTUAL
        /// </summary>
        /// 
        public async Task<TurnoViewModel1> GetTurnoActual(int idcaja)
        {
            var response = await client.GetAsync($"Turnos/TurnoActual/{idcaja}");

            if (response.IsSuccessStatusCode)
            {
                var jsonresponse = await response.Content.ReadAsStringAsync();
                var turno = JsonConvert.DeserializeObject<TurnoViewModel1>(jsonresponse);
                return turno;
            }
            return null;
        }

        public async Task<TurnoViewModel1> Avanzar(int id) //MODIFICAR PARA ID CLAIMS
        {
            var response = await client.GetAsync($"Turnos/Avanzar/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonresponse = await response.Content.ReadAsStringAsync();
                var turno = JsonConvert.DeserializeObject<TurnoViewModel1>(jsonresponse);
                return turno;

            }
            return null;
        }

		public async Task<TurnoViewModel1> Atrasar(int id)
        {
			var response = await client.GetAsync($"Turnos/Atrasar/{id}");

			if (response.IsSuccessStatusCode)
			{
				var jsonresponse = await response.Content.ReadAsStringAsync();
				var turno = JsonConvert.DeserializeObject<TurnoViewModel1>(jsonresponse);
				return turno;

			}
			return null;
		}


		///<summary>
		///CLIENTE********************
		/// </summary>
		/// 
		///<summary>
		///VER TODOS LOS TURNOS
		/// </summary>
		/// 
		public async Task<List<TurnoViewModel1>> GetAllTurnos()
        {
            List<TurnoViewModel1> listaTurnos = new();

            var response = await client.GetAsync($"Turnos");

            if (response.IsSuccessStatusCode)
            {
                var jsonresponse = await response.Content.ReadAsStringAsync();
                listaTurnos = JsonConvert.DeserializeObject<List<TurnoViewModel1>>(jsonresponse);
                return listaTurnos;
            }
            return null;
        }

        ///<summary>
        ///AVANZAR TURNO
        /// </summary>
        /// 

        //public async Task<TurnoViewModel1> Avanzar(int id) //MODIFICAR PARA ID CLAIMS
        //{
        //    var response = await client.GetAsync($"Avanzar/{id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonresponse = await response.Content.ReadAsStringAsync();
        //        var turno = JsonConvert.DeserializeObject<TurnoViewModel1>(jsonresponse);
        //        return turno;

        //    }
        //    return null;
        //}


        //////////////////////ESTADISTICAS/////////////////////////////////////
        ///
        public async Task<int> GetCajasActivas()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var response = await client.GetAsync($"Cajas/CajasActivas");

            if (response.IsSuccessStatusCode)
            {
                var total = await response.Content.ReadAsStringAsync();
                return ((int.Parse(total)));
            }
            return 0;
        }

        public async Task<int> GetCajasInactivas()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var response = await client.GetAsync($"Cajas/CajasInactivas");

            if (response.IsSuccessStatusCode)
            {
                var total = await response.Content.ReadAsStringAsync();
                return ((int.Parse(total)));
            }
            return 0;
        }

        public async Task<int> GetTotalTurnosEspera()
        {
            var response = await client.GetAsync($"Turnos/EnEspera");

            if (response.IsSuccessStatusCode)
            {
                var total = await response.Content.ReadAsStringAsync();
                return ((int.Parse(total)));
            }
            return 0;
        }

        public async Task<int> GetTotalTurnosAtendidos()
        {
            var response = await client.GetAsync($"Turnos/Atendidos");

            if (response.IsSuccessStatusCode)
            {
                var total = await response.Content.ReadAsStringAsync();
                return ((int.Parse(total)));
            }
            return 0;
        }
    }
}
