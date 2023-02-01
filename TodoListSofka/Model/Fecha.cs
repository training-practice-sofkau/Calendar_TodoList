using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public partial class Fecha
{
    public Guid Id { get; set; }

    public DateTime Fecha1 { get; set; }

    public int? Dia { get; set; }

    public int? Mes { get; set; }

    public int? Año { get; set; }

    public bool State { get; set; }

    public Guid IdEventos { get; set; }

    public virtual Tarea IdEventosNavigation { get; set; } = null!;
}
