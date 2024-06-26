﻿namespace API_Linea_Espera.Models.DTOs
{
    public class CajaDTO
    {
        public int Id { get; set; }
        public string NombreCaja { get; set; } = null!;
        public sbyte? Estado { get; set; }
        public int? IdTurno { get; set; }
        public string? NombreUsuario { get; set; }
        public string? EstadoTurno { get; set; }
    }
}
