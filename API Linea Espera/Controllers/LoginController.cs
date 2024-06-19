using API_Linea_Espera.Helpers;
using API_Linea_Espera.Models.DTOs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using FluentValidation.Results;
using API_Linea_Espera.Models.Validators;

namespace API_Linea_Espera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        public IRepository<Usuarios> RepositoryUsuarios { get; }
        public IRepository<Cajas> RepositoryCajas { get; }
        public IRepository<Roles> RepositoryRoles { get; }
        public TokenGeneratorJwt JwtTokenGenerator { get; }
        readonly UsuarioValidator usuarioValidator;

        public LoginController(
        IRepository<Usuarios> repositoryUsuarios,
        IRepository<Cajas> repositoryCajas,
        IRepository<Roles> repositoryRoles,
        UsuarioValidator usuarioValidator,
        TokenGeneratorJwt jwtTokenGenerator)
        {
            this.RepositoryUsuarios = repositoryUsuarios;
            this.RepositoryCajas = repositoryCajas;
            this.RepositoryRoles = repositoryRoles;
            this.usuarioValidator = usuarioValidator;
            this.JwtTokenGenerator = jwtTokenGenerator;
        }


        [HttpPost]
        public IActionResult Post([FromBody] UsuarioDTO usuarioDTO)
        {
            
            var validationResult = usuarioValidator.Validate(usuarioDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            try
            {
                var pass = ConvertPasswordToSHA512(usuarioDTO.Contraseña);
                var usuario = RepositoryUsuarios.GetAllWithInclude()
                    .FirstOrDefault(x => x.NombreUsuario == usuarioDTO.NombreUsuario && x.Contraseña == pass);

                if (usuario == null)
                {
                    return NotFound("Usuario o contraseña incorrectos.");
                }

                var rol = RepositoryRoles.GetAll().FirstOrDefault(x => x.IdRol == usuario.IdRol);
                if (rol == null)
                {
                    return NotFound("Rol del usuario no encontrado.");
                }

                string token = rol.NombreRol == "Operador"
                    ? JwtTokenGenerator.GetToken(usuario.Nombre, rol.NombreRol, usuario.Id.ToString(), usuario.IdCaja.ToString())
                    : JwtTokenGenerator.GetToken(usuario.Nombre, rol.NombreRol, usuario.Id.ToString(), null);

                //return Ok(new { token });
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
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
