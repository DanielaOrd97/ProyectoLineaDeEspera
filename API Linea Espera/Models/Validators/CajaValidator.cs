using API_Linea_Espera.Models.DTOs;
using FluentValidation;
using FluentValidation.Internal;
using System.Data;

namespace API_Linea_Espera.Models.Validators
{
    public class CajaValidator:AbstractValidator<CajaDTO>
    {
     public CajaValidator()
        {
            RuleFor(x => x.NombreCaja).NotEmpty()
                .WithMessage("El nombre de la caja no puede estar vacío. ")
                .Length(4,50).WithMessage("El nombre de la caja debe tener entre 4 y 50 caracteres. ");

            RuleFor(x => x.Estado).InclusiveBetween((sbyte)0, (sbyte)1).WithMessage("el estado debe ser 0 (inactivo) o 1 (activo). ")
                .When(x=>x.Estado.HasValue);
            RuleFor(x => x.Estado).NotEmpty()
                .WithMessage("El estado no puede estar vacío");

            RuleFor(x => x.NombreUsuario).NotEmpty()
                .WithMessage("el nombre del usuario no puede estar vacío. ");

            RuleFor(x=>x.NombreUsuario)
                .MaximumLength(50).WithMessage("El nombre de usuario solo puede tener 50 caracteres. ")
                .When(x=>!string.IsNullOrEmpty(x.NombreUsuario));

            RuleFor(x=>x.EstadoTurno)
                .MaximumLength(50).WithMessage("El estado del turno puede tener hasta 50 caracteres. ").
                When(x=>!string.IsNullOrEmpty(x.EstadoTurno));
            
           
        }   
    }
}
