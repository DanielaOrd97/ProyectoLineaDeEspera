using AdminApp.Models.ViewModels;
using FluentValidation;

namespace AdminApp.Areas.Administrador.Models.Validators
{
    public class CajaAdminValidator:AbstractValidator<CajaViewModel1>
    {
        public CajaAdminValidator() {
            RuleFor(x => x.NombreCaja).NotEmpty()
                   .WithMessage("El nombre de la caja es obligatorio");
            RuleFor(x => x.Estado).NotNull().WithMessage("El estado es obligatorio").
                InclusiveBetween((sbyte)0, (sbyte)1).WithMessage("El estado debe ser 0 (No activa) o 1 (Activa)");

        }
    }
}
