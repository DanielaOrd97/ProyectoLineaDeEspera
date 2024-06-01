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

    }
}
