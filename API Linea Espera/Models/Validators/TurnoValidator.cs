using API_Linea_Espera.Models.DTOs;
using FluentValidation;

namespace API_Linea_Espera.Models.Validators
{
    public class TurnoValidator:AbstractValidator<TurnoDTO>
    {
        public TurnoValidator() 
        {
            RuleFor(x => x.NombreCliente)
                .MaximumLength(100).WithMessage("El nombre del cliente puede tener hasta 100 caracteres. ")
                .When(x => !string.IsNullOrEmpty(x.NombreCliente));
            RuleFor(x => x.NombreCaja)
                .MaximumLength(50).WithMessage("El nombre de la caja puede tener hasta 50 caracteres")
                .When(x=>!string.IsNullOrEmpty(x.NombreCaja));
            RuleFor(x => x.Posicion)
                .GreaterThanOrEqualTo(0).WithMessage("La posición debe ser mayor o igual a 0. ");

        }
    }
}
