using AdminApp.Models.ViewModels;
using FluentValidation;

namespace AdminApp.Areas.Administrador.Models.Validators
{
    public class UsuariosAdminValidator:AbstractValidator<AgregarUsuarioViewModel1>
    {

        public UsuariosAdminValidator()
        {
			RuleFor(x => x.NombreUsuario).NotEmpty()
				.WithMessage("El nombre de usuario no puede estar vacío. ")
				.MaximumLength(50).WithMessage("El nombre del usuario no puede exceder los 50 caracteres");
			//RuleFor(x => x.Contraseña).NotEmpty()
			//	.WithMessage("La contraseña es obligatoria. ")
			//	.MinimumLength(6).WithMessage("La contraseña debe de tener ak menos 6 caracteres ")
			//	.MaximumLength(50).WithMessage("La contraseña no puede exceder los 50 caracteres");
			RuleFor(x => x.Nombre)
				.NotEmpty().WithMessage("El nombre es obligatorio")
				.MaximumLength(100).WithMessage("el nombre no puede exceder los 100 caracteres");
			//RuleFor(x => x.IdRol).NotEmpty()
			//	.WithMessage("El rol es obligatorio");

			//	.GreaterThan(0).WithMessage("Seleccione una caja valida");
			//RuleFor(x => x.IdCaja).GreaterThanOrEqualTo(0).WithMessage("Seleccione una caja valida");
		}

		//public AgregarUsuarioValidator()
		//{
		//    RuleFor(x => x.NombreUsuario).NotEmpty()
		//        .WithMessage("El nombre de usuario no puede estar vacío. ")
		//        .MaximumLength(50).WithMessage("El nombre del usuario no puede exceder los 50 caracteres");
		//    RuleFor(x => x.Contraseña).NotEmpty()
		//        .WithMessage("La contraseña es obligatoria. ")
		//        .MinimumLength(6).WithMessage("La contraseña debe de tener ak menos 6 caracteres ")
		//        .MaximumLength(50).WithMessage("La contraseña no puede exceder los 50 caracteres");
		//    RuleFor(x => x.Nombre)
		//        .NotEmpty().WithMessage("El nombre es obligatorio")
		//        .MaximumLength(100).WithMessage("el nombre no puede exceder los 100 caracteres");
		//    RuleFor(x => x.IdRol).NotEmpty()
		//        .WithMessage("El rol es obligatorio")
		//        .GreaterThan(0).WithMessage("Seleccione una caja valida");
		//    RuleFor(x => x.IdCaja).GreaterThanOrEqualTo(0).WithMessage("Seleccione una caja valida");
		//}
	}
}
