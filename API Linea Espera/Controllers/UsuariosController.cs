using API_Linea_Espera.Models.DTOs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Linea_Espera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IRepository<Usuarios> Repository { get; }
        public UsuariosController(IRepository<Usuarios> repository)
        {
            this.Repository = repository;
        }

        private UsuarioDTO MapToDto(Usuarios usuarios)
        {
            return new UsuarioDTO
            {
                Id = usuarios.Id,
                NombreUsuario = usuarios.NombreUsuario,
                Contraseña = usuarios.Contraseña,
                Nombre = usuarios.Nombre,
                FechaDeRegistro = usuarios.FechaDeRegistro,
                IdRol = usuarios.IdRol ?? 0,
                NombreRol = usuarios.IdRolNavigation?.NombreRol ?? "",
                IdCaja = usuarios.IdCaja ?? 0,
                NombreCaja = usuarios.IdCajaNavigation?.NombreCaja ?? ""
            };
        }


        private Usuarios MapToEntity(UsuarioDTO dto, Usuarios? original = null)
        {
            if (original == null)
            {
                original = new Usuarios();
            }

            return new Usuarios
            {
                Id = dto.Id ?? 0,
                NombreUsuario = dto.NombreUsuario,
                Contraseña = dto.Contraseña,
                Nombre = dto.Nombre,
                FechaDeRegistro = DateTime.Now,
                IdRol = dto.IdRol,
                IdCaja = dto.IdCaja
            };
        }

        /// <summary>
        /// MOSTRAR SOLO AQUELLOS QUE SON OPERADORES. (ACCION PARA EL ADMIN)
        /// </summary>

        [HttpGet("Operadores")]
        public IActionResult GetAll()
        {
            var operadores = Repository.GetAllWithInclude()
                .Where(x => x.IdRol == 2)
                .OrderBy(x => x.Id)
                .Select(x => MapToDto(x))
                ;

            return Ok(operadores);
        }

        ///<summary>
        ///BUSCAR UN OPERADOR EN ESPECIFICO.  (ACCION PARA EL ADMIN)
        /// </summary>
        [HttpGet("Operador/{id}")]
        public IActionResult GetOperador(int id)
        {
            //var operador = Repository.Get(id);
            var operador  = Repository.GetAllWithInclude()
                .First(x => x.Id == id);

            if (operador == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(operador));
        }

        ///<summary>
        ///AGREGAR UN OPERADOR.  (ACCION PARA EL ADMIN)
        /// </summary>
        [HttpPost("AgregarOperador")]
        public IActionResult PostOperador(UsuarioDTO dto)
        {
            if (dto != null)
            {
                dto.IdRol = 2; //Asignar rol operador.
                var operador = MapToEntity(dto);
                Repository.Insert(operador);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        ///<summary>
        ///EDITAR UN OPERADOR.  (ACCION PARA EL ADMIN)
        /// </summary>
        /// 
        [HttpPut("EditarOperador")]
        public IActionResult PutOperador(UsuarioDTO dto)
        {
            if (dto != null)
            {
                var operador = Repository.GetAllWithInclude()
               .First(x => x.Id == dto.Id);

                if (operador != null)
                {

                    operador.NombreUsuario = dto.NombreUsuario;
                    operador.Contraseña = dto.Contraseña;
                    operador.Nombre = dto.Nombre;
                    operador.FechaDeRegistro = operador.FechaDeRegistro;
                    operador.IdRol = dto.IdRol;
                    operador.IdCaja = dto.IdCaja;

                    if (dto.IdCaja == null)
                    {
                        operador.IdCaja = operador.IdCaja;
                    }
                    else
                    {
                        operador.IdCaja = dto.IdCaja;
                    }

                    Repository.Update(operador);
                    return Ok();
                }
            }
            return NotFound();
        }

    }
}
