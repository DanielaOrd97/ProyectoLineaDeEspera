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
    }
}
