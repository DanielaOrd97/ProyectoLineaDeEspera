using System;
using System.Collections.Generic;

namespace API_Linea_Espera.Models.Entities;

public partial class Estadosturno
{
    public int IdEstado { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Turnos> Turnos { get; set; } = new List<Turnos>();
}
