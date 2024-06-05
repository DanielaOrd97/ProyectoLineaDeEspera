using API_Linea_Espera.Models.DTOs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Linea_Espera.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		public IRepository<Roles> Repository { get; }

		public RolesController(IRepository<Roles> repository)
        {
			this.Repository = repository;
		}

		[HttpGet]
		public IActionResult GetAllRoles()
		{
			var roles = Repository.GetAll()
				.OrderBy(x => x.IdRol)
				.Select(x => new RolesDTO
				{
					Id = x.IdRol,
					NombreRol = x.NombreRol
				});

			return Ok(roles);
		}
	}
}
