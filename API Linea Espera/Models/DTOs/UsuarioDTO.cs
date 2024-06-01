using System.Reflection.Metadata.Ecma335;

namespace API_Linea_Espera.Models.DTOs
{
    public class UsuarioDTO
    {
        public int? Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string? Nombre { get; set; }
        public DateTime? FechaDeRegistro { get; set; }
        public int IdRol { get; set; }
        public string? NombreRol { get; set; }
        public int? IdCaja { get; set; }
        public string? NombreCaja { get; set; }
    }
}
