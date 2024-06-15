using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCLIENTEPRUEBA1.Models.DTOs
{
    public class CajaDTO
    {
        public int Id { get; set; }
        public string NombreCaja { get; set; } = null!;
        public sbyte? Estado { get; set; }
    }
}
