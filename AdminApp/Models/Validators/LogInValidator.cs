using AdminApp.Models.ViewModels;
using FluentValidation;

namespace AdminApp.Models.Validators
{
    public class LogInValidator:AbstractValidator<LogInViewModel>
    {
        public LogInValidator() { 
        RuleFor(x=>x.NombreUsuario).NotEmpty()
                .WithMessage("El nombre de usuario es obligatorio. ")
                .Length(3,50).WithMessage("El nombre de usuario debe tener entre 3 y 50 caracteres. ");
            RuleFor(x => x.Contraseña).NotEmpty()
                .WithMessage("La contraseña es obligatoria. ");
                //.Length(6,100).WithMessage("La contraseña debe de tener entre 6 y 100 caracteres");
        }
    }
}
