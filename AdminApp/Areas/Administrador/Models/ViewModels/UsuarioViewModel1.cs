namespace AdminApp.Areas.Administrador.Models.ViewModels
{
	public class UsuarioViewModel1
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
