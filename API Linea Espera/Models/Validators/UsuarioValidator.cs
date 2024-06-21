using API_Linea_Espera.Models.DTOs;
using API_Linea_Espera.Models.Entities;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using System.Drawing;

namespace API_Linea_Espera.Models.Validators
{
    public class UsuarioValidator:AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator() {
            //RuleFor(x => x.Nombre).NotEmpty()
            //        .WithMessage("El nombre del usuario no puede estar vacío")
            //        .MaximumLength(100)
            //        .WithMessage("El nombre puede tener hasta 100 caracteres. ");
            RuleFor(x => x.NombreUsuario).NotEmpty()
                .WithMessage("El nombre del usuario no puede estar vacío. ")
                .Length(4, 50)
                .WithMessage("El nombre del usuario debe tener entre 3 y 50 caracteres. ");
            RuleFor(x => x.Contraseña).NotEmpty()
                .WithMessage("La contraseña no puede estar vacía. ");
            //ESTO PUEDE SER OPCIONAL
            //.MinimumLength(6)
            //.WithMessage("La contraseña debe tener al menos 6 caracteres para ser valida");
            //RuleFor(x => x.FechaDeRegistro).LessThanOrEqualTo(DateTime.UtcNow)
            //.WithMessage("La fecha de registro no puede ser en el futuro. ")
            //.When(x => x.FechaDeRegistro.HasValue);
            //RuleFor(x => x.NombreRol).MaximumLength(50).WithMessage("El nombre del rol puede tener hasta 50 caracteres. ")
            //    .When(x => !string.IsNullOrEmpty(x.NombreRol));
            //RuleFor(x => x.NombreRol).NotEmpty()
            //    .WithMessage("El rol no puede estar vacío. ");
            //RuleFor(x => x.NombreCaja).NotEmpty()
            //    .WithMessage("El nombre de la caja no puede estar vacío. ");
            //RuleFor(x=>x.NombreCaja)
            //    .MaximumLength(50).WithMessage("El nombre de la caja puede tener hasta 50 caracteres. ")
            //    .When(x=>!string.IsNullOrEmpty(x.NombreCaja));


         //Validaciones propuestas:
         //NombreUsuario: No puede estar vacío y debe tener entre 3 y 50 caracteres.
         //Contraseña: No puede estar vacía y debe tener al menos 6 caracteres.
         //Nombre: Puede tener hasta 100 caracteres.
         //FechaDeRegistro: No puede ser una fecha futura(solo se valida si tiene valor).
         //IdRol: Debe ser mayor a 0.
         //NombreRol: Puede tener hasta 50 caracteres(solo se valida si no está vacío).
         //IdCaja: Debe ser mayor a 0(solo se valida si tiene valor).
         //NombreCaja: Puede tener hasta 50 caracteres(solo se valida si no está vacío).


        }
    }
}
