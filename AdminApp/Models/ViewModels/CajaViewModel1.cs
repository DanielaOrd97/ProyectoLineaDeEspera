namespace AdminApp.Models.ViewModels
{
    public class CajaViewModel1
    {
        public int Id { get; set; }
        public string NombreCaja { get; set; } = null!;
        public sbyte? Estado { get; set; }
        public int? IdTurno { get; set; }
        public string? NombreUsuario { get; set; }
        public string? EstadoTurno { get; set; }
        public string? Error { get; set; } = "";
    }
}
