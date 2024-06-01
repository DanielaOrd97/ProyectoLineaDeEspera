namespace API_Linea_Espera.Models.DTOs
{
    public class TurnoDTO
    {
        public int? IdTurno { get; set; }
        public string? NombreCliente { get; set; }
        public string? NombreCaja { get; set; }
        public string EstadoTurno { get; set; }
    }
}
