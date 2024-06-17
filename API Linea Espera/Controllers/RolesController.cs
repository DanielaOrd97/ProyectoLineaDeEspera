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
		readonly API_Linea_Espera.Models.Validators.RolValidator rolValidator;
		public RolesController(IRepository<Roles> repository,
			API_Linea_Espera.Models.Validators.RolValidator rolValidator
			)
        {
			this.Repository = repository;
			this.rolValidator = rolValidator;
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
