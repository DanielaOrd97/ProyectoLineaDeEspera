using AdminApp.Models.ViewModels;

namespace AdminApp.Models.ViewModels
{
    public class AgregarUsuarioViewModel1
    {
        public int? Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string? Nombre { get; set; }
        public int IdRol { get; set; }
        public int? IdCaja { get; set; }
        public IEnumerable<RolViewModel1>? ListaRoles { get; set; }
        public IEnumerable<CajaViewModel1>? ListaCajas { get; set; }
        public string? Error { get; set; }
    }
}
