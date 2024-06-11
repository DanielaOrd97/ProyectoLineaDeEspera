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
        public static int UltimaPosicion { get; set; } 
        public TurnosController(IRepository<Turnos> repository)
        {
            this.Repository = repository;
            UltimaPosicion = UltimaPosicion;
        }

        ///<summary>
        ///VER TURNOS.
        /// </summary>
        /// 
        [HttpGet]
        public IActionResult GetAllTurnos()
        {
            var turnos = Repository.GetAllTurnosWithInclude()
                .Select(x => new TurnoDTO
                {
                    IdTurno = x.IdTurno,
                    //NombreCliente = x.Usuario.Nombre,
                    NombreCaja = x.Caja.NombreCaja,
                    EstadoTurno = x.Estado.Estado,
                    Posicion = x.Posicion
                })
                .OrderBy(x => x.Posicion);

            //UltimaPosicion =  turnos.Last().Posicion;

            return Ok(turnos);
        }

        ///<summary>
        ///VER TURNOS POR CAJA
        /// </summary>
        /// 
        [HttpGet("TurnosPorCaja")]
        public IActionResult GetTurnosPorCaja()
        {
            var turnos = Repository.GetAllTurnosWithInclude()
                 .Select(x => new TurnoDTO
                 {
                     IdTurno = x.IdTurno,
                     NombreCliente = x.Usuario.Nombre,
                     IdCaja = x.CajaId,
                     NombreCaja = x.Caja.NombreCaja,
                     EstadoTurno = x.Estado.Estado,
                     Posicion = x.Posicion
                 })
                .OrderBy(x => x.Posicion)
                .GroupBy(x => x.IdCaja);

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
                    EstadoId = dto.IdEstado,
                    Posicion = UltimaPosicion+=1
                };

                UltimaPosicion = entity.Posicion;

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
                    EstadoTurno = x.Estado.Estado,
                    Posicion = x.Posicion
                })
                .OrderBy(x => x.Posicion);

                UltimaPosicion = turnos.Last().Posicion;

                return Ok(turnos);
            }

            return NotFound();
        }

        /// <summary>
        /// ADELANTAR TURNO. 
        /// </summary>

        //[HttpPost("Adelantar/{id}")]
        //public IActionResult AdelantarTurno(int id)
        //{
        //    var turnos = Repository.GetAllTurnosWithInclude()
        //        .OrderBy(x => x.Posicion)
        //        .ToList();

        //    var turnoseleccionado = Repository.GetAllTurnosWithInclude()
        //        .FirstOrDefault(x => x.IdTurno == id);


        //    if(turnoseleccionado != null)
        //    {
        //        var posicion = turnos.IndexOf(turnoseleccionado);

        //        if (posicion == 0)
        //        {
        //            return BadRequest();
        //        }

        //        var posicionanterior = turnos[posicion - 1];

        //        var posicionTemp = turnoseleccionado.Posicion;
        //        turnoseleccionado.Posicion = posicionanterior.Posicion;
        //        posicionanterior.Posicion = posicionTemp;


        //        Repository.Update(turnoseleccionado);
        //        Repository.Update(posicionanterior);

        //        return Ok();
        //    }

        //    return BadRequest();
        //}

        /// <summary>
        /// ATRASAR TURNO.
        /// </summary>

        //[HttpPost("/Atrasar/{id}")]
        //public IActionResult AtrasarTurno(int id)
        //{
        //    var turnos = Repository.GetAllTurnosWithInclude()
        //        .OrderBy(x => x.Posicion).ToList();

        //    var turnoseleccionado = Repository.GetAllTurnosWithInclude()
        //        .FirstOrDefault(x => x.IdTurno == id);


        //    if (turnoseleccionado != null)
        //    {
        //        var posicion = turnos.IndexOf(turnoseleccionado);

        //        if (posicion == 0)
        //        {
        //            return BadRequest();
        //        }

        //        var posicionsig = turnos[posicion + 1];

        //        var posicionTemp = turnoseleccionado.Posicion;
        //        turnoseleccionado.Posicion = posicionsig.Posicion;
        //        posicionsig.Posicion = posicionTemp;


        //        Repository.Update(turnoseleccionado);
        //        Repository.Update(posicionsig);

        //        return Ok();
        //    }

        //    return BadRequest();
        //}

        ///<summary>
        ///VER TURNO ACTUAL
        /// </summary>
        /// 
        [HttpGet("TurnoActual/{id}")]
        public IActionResult GetTurnoActual(int id)
        {
            var turnoactual = Repository.GetAllTurnosWithInclude()
                .Where(x => x.CajaId == id)
                .Select(x => new TurnoDTO
                {
                    IdTurno = x.IdTurno,
                    NombreCaja = x.Caja.NombreCaja,
                    Posicion = x.Posicion
                })
                .FirstOrDefault();

            UltimaPosicion = turnoactual.Posicion;

            return Ok(turnoactual);
        }

        ///<summary>
        ///AVANZAR TURNO.
        /// </summary>
        /// 
        [HttpGet("Avanzar/{id}")]
        public IActionResult AdelantarTurno(int id)
        {
            //Ver solo turnos de la caja que tiene el id del parametro.
            var turnosig = Repository.GetAllTurnosWithInclude()
                .Where(x => x.CajaId == id && x.Posicion > UltimaPosicion)
                .Select(x => new TurnoDTO
                {
					IdTurno = x.IdTurno,
					NombreCaja = x.Caja.NombreCaja,
                    Posicion = x.Posicion
				})
            .FirstOrDefault();

            UltimaPosicion = turnosig.Posicion;

			return Ok(turnosig);
		}

		[HttpGet("Atrasar/{id}")]
		public IActionResult AtrasarTurno(int id)
		{
			//Ver solo turnos de la caja que tiene el id del parametro.
			var turnoanterior = Repository.GetAllTurnosWithInclude()
                .OrderByDescending(x => x.IdTurno)
				.Where(x => x.CajaId == id && UltimaPosicion > x.Posicion)
				.Select(x => new TurnoDTO
				{
					IdTurno = x.IdTurno,
					NombreCaja = x.Caja.NombreCaja,
					Posicion = x.Posicion
				})
			.FirstOrDefault();

			UltimaPosicion = turnoanterior.Posicion;

			return Ok(turnoanterior);
		}
	}
}
