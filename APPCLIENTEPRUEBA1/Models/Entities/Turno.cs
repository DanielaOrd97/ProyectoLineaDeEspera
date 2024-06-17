using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace APPCLIENTEPRUEBA1.Models.Entities
{
    [Table("Turnos")]
    public class Turno
    {
        [PrimaryKey]
        public int IdTurno { get; set; }

       // public int? UsuarioId { get; set; }

        //public int? CajaId { get; set; }

        //public int? EstadoId { get; set; }

        public int Posicion { get; set; }
        public string? NombreCaja { get; set; }
        public string? EstadoTurno { get; set; }

        //public virtual Cajas? Caja { get; set; }

        //public virtual Estadosturno? Estado { get; set; }

        //public virtual Usuarios? Usuario { get; set; }
    }
}
