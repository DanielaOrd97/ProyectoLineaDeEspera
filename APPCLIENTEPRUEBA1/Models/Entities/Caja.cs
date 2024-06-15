using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace APPCLIENTEPRUEBA1.Models.Entities
{
    [Table("Cajas")]
    public class Caja
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NotNull]
        public string NombreCaja { get; set; } = null!;

        [NotNull]
        public byte Estado { get; set; }
    }
}
