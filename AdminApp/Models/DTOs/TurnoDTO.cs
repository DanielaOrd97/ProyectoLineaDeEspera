﻿namespace AdminApp.Models.DTOs
{
    public class TurnoDTO
    {
        public int? IdTurno { get; set; }
        public string? NombreCliente { get; set; }
        public string? NombreCaja { get; set; }
        public string? EstadoTurno { get; set; }
        public int? IdCliente { get; set; }
        public int? IdCaja { get; set; }
        public int? IdEstado { get; set; }
        public int Posicion { get; set; }
    }
}
