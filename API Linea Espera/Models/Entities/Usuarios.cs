using System;
using System.Collections.Generic;

namespace API_Linea_Espera.Models.Entities;

public partial class Usuarios
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string? Nombre { get; set; }

    public DateTime? FechaDeRegistro { get; set; }

    public int? IdRol { get; set; }

    public int? IdCaja { get; set; }

    public virtual Cajas? IdCajaNavigation { get; set; }

    public virtual Roles? IdRolNavigation { get; set; }

    public virtual ICollection<Turnos> Turnos { get; set; } = new List<Turnos>();
}
