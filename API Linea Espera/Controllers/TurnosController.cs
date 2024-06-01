using API_Linea_Espera.Models.DTOs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Linea_Espera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        public IRepository<Turnos> Repository { get; }
        public TurnosController(IRepository<Turnos> repository)
        {
            this.Repository = repository;
        }

        ///<summary>
        ///VER DETALLES DE TURNO.
        /// </summary>
        /// 
        [HttpGet]
        public IActionResult GetAllTurnos()
        {
            var turnos = Repository.GetAllTurnosWithInclude()
                .Select(x => new TurnoDTO
                {
                    IdTurno = x.IdTurno,
                    NombreCliente = x.Usuario.Nombre,
                    NombreCaja = x.Caja.NombreCaja,
                    EstadoTurno = x.Estado.Estado
                });

            return Ok(turnos);
        }

        ///<summary>
        ///AGREGAR TURNO.
        /// </summary>
        /// 
        [HttpPost]
        public IActionResult PostTurno(TurnoDTO dto)
        {
            if(dto != null)
            {
                Turnos entity = new()
                {
                    IdTurno = 0,
                    UsuarioId = dto.IdCliente,
                    CajaId = dto.IdCaja,
                    EstadoId = dto.IdEstado
                };

                Repository.Insert(entity);
                return Ok();
            }

            return BadRequest();
        }

        ///<summary>
        ///VER TURNOS DE UNA CAJA ESPECIFICA.
        /// </summary>
        /// 
        [HttpGet("{id}")]
        public IActionResult GetTurnosEspecificos(int id)
        {
            if(id != 0)
            {
                var turnos = Repository.GetAllTurnosWithInclude()
                .Where(x => x.CajaId == id)
                .Select(x => new TurnoDTO
                {
                    IdTurno = x.IdTurno,
                    NombreCliente = x.Usuario.Nombre,
                    NombreCaja = x.Caja.NombreCaja,
                    EstadoTurno = x.Estado.Estado
                });

                return Ok(turnos);
            }

            return NotFound();
        }
    }
}
