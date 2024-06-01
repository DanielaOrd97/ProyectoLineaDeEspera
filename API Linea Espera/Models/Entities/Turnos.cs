using System;
using System.Collections.Generic;

namespace API_Linea_Espera.Models.Entities;

public partial class Turnos
{
    public int IdTurno { get; set; }

    public int? UsuarioId { get; set; }

    public int? CajaId { get; set; }

    public int? EstadoId { get; set; }

    public virtual Cajas? Caja { get; set; }

    public virtual Estadosturno? Estado { get; set; }

    public virtual Usuarios? Usuario { get; set; }
}
