using API_Linea_Espera.Helpers;
using API_Linea_Espera.Models.DTOs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API_Linea_Espera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        public IRepository<Usuarios> RepositoryUsuarios { get; }
        public IRepository<Cajas> RepositoryCajas { get; }
        public IRepository<Roles> RepositoryRoles { get; }
        public LoginController(IRepository<Usuarios> repositoryUsuarios, 
            IRepository<Cajas> repositoryCajas, IRepository<Roles> repositoryRoles)
        {
            this.RepositoryUsuarios = repositoryUsuarios;
            this.RepositoryCajas = repositoryCajas;
            this.RepositoryRoles = repositoryRoles;
        }


        [HttpPost]
        public IActionResult Post(UsuarioDTO usuarioDTO)
        {
            if (string.IsNullOrWhiteSpace(usuarioDTO.NombreUsuario))
            {
                ModelState.AddModelError("", "Proporcione el nombre de usuario para iniciar sesión");
                return BadRequest(ModelState);

            }
            if (string.IsNullOrWhiteSpace(usuarioDTO.Contraseña))
            {
                ModelState.AddModelError("", "Proporcione la contraseña para iniciar sesión");
                return BadRequest(ModelState);
            }

            var pass = ConvertPasswordToSHA512(usuarioDTO.Contraseña);
            var usuario = RepositoryUsuarios.GetAllWithInclude()
                .First(x => (x.NombreUsuario == usuarioDTO.NombreUsuario) && (x.Contraseña == pass));

            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                var rol = RepositoryRoles.GetAll().First(x => x.IdRol == usuario.IdRol);

                TokenGeneratorJwt jwtToken = new();

                if (rol.NombreRol == "Operador")
                {
                    return Ok(jwtToken.GetToken(usuario.Nombre, rol.NombreRol, usuario.Id.ToString(), usuario.IdCaja.ToString()));
                }


                return Ok(jwtToken.GetToken(usuario.Nombre, rol.NombreRol, usuario.Id.ToString(), null));
                
            }
        }


        ///<summary>
        ///METODO PARA COMPARAR CONTRA ENCRIPTADA.
        /// </summary>
        /// 
        public static string ConvertPasswordToSHA512(string password)
        {
            using (var sha512 = SHA512.Create())
            {
                var arreglo = Encoding.UTF8.GetBytes(password);
                var hash = sha512.ComputeHash(arreglo);
                return Convert.ToHexString(hash).ToLower();
            }
        }
    }
}
