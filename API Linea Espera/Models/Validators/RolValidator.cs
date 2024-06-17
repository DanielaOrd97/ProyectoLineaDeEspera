using API_Linea_Espera.Models.DTOs;
using FluentValidation;

namespace API_Linea_Espera.Models.Validators
{
    public class RolValidator:AbstractValidator<RolesDTO>
    {
        public RolValidator() { 
        RuleFor(x=>x.NombreRol).NotEmpty()
                .WithMessage("El nombre del rol no puede estar vacío. ")
                .MaximumLength(50)
                .WithMessage("El nombre del rol puede tener hasta 50 caracteres. ");
           
        }
    }
}
