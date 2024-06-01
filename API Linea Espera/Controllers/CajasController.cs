using API_Linea_Espera.Models.DTOs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Linea_Espera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajasController : ControllerBase
    {
        public IRepository<Cajas> Repository { get; }
        public CajasController(IRepository<Cajas> repository)
        {
            this.Repository = repository;
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
                    Estado = x.Estado
                });

            return Ok(cajas);
        }

        ///<summary>
        ///AGREGAR CAJA
        /// </summary>
        /// 
        [HttpPost]
        public IActionResult PostCaja(CajaDTO dto)
        {
            if (dto != null)
            {
                Cajas entity = new()
                {
                    IdCaja = 0,
                    NombreCaja = dto.NombreCaja,
                    Estado = dto.Estado
                };

                Repository.Insert(entity);
                return Ok();
            }
            return BadRequest();
        }

        ///<summary>
        ///EDITAR CAJA
        /// </summary>
        /// 
        [HttpPut]
        public IActionResult PutCaja(CajaDTO dto)
        {
            if (dto != null)
            {
                var caja = Repository.Get(dto.Id);

                if (caja != null)
                {
                    caja.NombreCaja = dto.NombreCaja;
                    caja.Estado = dto.Estado;

                    Repository.Update(caja);
                    return Ok();
                }

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
    }
}
