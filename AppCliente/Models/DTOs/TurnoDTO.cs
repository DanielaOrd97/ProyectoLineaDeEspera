using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCliente.Models.DTOs
{
    public class TurnoDTO
    {
        public int? IdTurno { get; set; }
        public int? IdCaja { get; set; }
        public string? NombreCaja { get; set; }
        public string? EstadoTurno { get; set; }
        public int Posicion { get; set; }


    }
}
