using API_Linea_Espera.Models.DTOs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_Linea_Espera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajasController : ControllerBase
    {
        public IRepository<Cajas> Repository { get; }
        readonly API_Linea_Espera.Models.Validators.CajaValidator cajasValidator;
        public CajasController(IRepository<Cajas> repository, 
            API_Linea_Espera.Models.Validators.CajaValidator cajasValidator)
        {
            this.Repository = repository;
           this.cajasValidator = cajasValidator;
            
        }

        ///<summary>
        ///VER TODAS LAS CAJAS
        ///</summary>
        [HttpGet]
        public IActionResult GetAllCajas()
        {
            var cajas = Repository.GetAll()
                .Select(x => new CajaDTO
                {
                    Id = x.IdCaja,
                    NombreCaja = x.NombreCaja,
                    Estado = x.Estado,
                    EstadoTurno = x.Estado == 0 ? "Inactiva" : "Activa"
                });;

            return Ok(cajas);
        }

        ///<summary>
        ///VER CAJA.
        /// </summary>
        /// 
        [HttpGet("{id}")]
        public IActionResult GetCaja(int id)
        {
            var caja = Repository.GetAll()
                .First(x => x.IdCaja == id);

            CajaDTO c = new()
            {
                Id = caja.IdCaja,
                NombreCaja = caja.NombreCaja,
                Estado = caja.Estado
            };

            if (caja == null)
            {
                return NotFound();
            }

            return Ok(c);
        }

        ///<summary>
        ///AGREGAR CAJA
        /// </summary>
        /// 
        [HttpPost]
        public IActionResult PostCaja(CajaDTO dto)
        {
            var validationResult=cajasValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);

            }
                Cajas entity = new()
                {
                    IdCaja = 0,
                    NombreCaja = dto.NombreCaja,
                    Estado = dto.Estado
                };

                Repository.Insert(entity);
                return Ok();
        }

        ///<summary>
        ///EDITAR CAJA
        /// </summary>
        /// 
        [HttpPut]
        public IActionResult PutCaja(CajaDTO dto)
        {
            var validationResult=cajasValidator.Validate(dto);
            if (!validationResult.IsValid) { 
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            
            }
            
                var caja = Repository.Get(dto.Id);

                if (caja != null)
                {
                    caja.NombreCaja = dto.NombreCaja;
                    caja.Estado = dto.Estado;

                    Repository.Update(caja);
                    return Ok();
                }
            return NotFound();
        }


        ///<summary>
        ///ELIMINAR CAJA
        /// </summary>
        /// 
        [HttpDelete("{id}")]
        public IActionResult DeleteCaja(int id)
        {
            var caja = Repository.Get(id);

            if (caja != null)
            {
                Repository.Delete(caja);
                return Ok();
            }

            return NotFound();
        }

        ///<summary>
        ///VER TOTAL DE CAJAS ACTIVAS
        /// </summary>
        /// 
        [HttpGet("CajasActivas")]
        public IActionResult VerTotalCajasActivas()
        {
            var activas = Repository.GetAll()
                .Where(x => x.Estado == 1)
                .Count();
            return Ok(activas);
        }

        ///<summary>
        ///VER TOTAL DE CAJAS INACTIVAS
        /// </summary>
        /// 
        [HttpGet("CajasInactivas")]
        public IActionResult VerTotalCajasInactivas()
        {
            var inactivas = Repository.GetAll()
                .Where(x => x.Estado == 0)
                .Count();
            return Ok(inactivas);
        }
    }
}
