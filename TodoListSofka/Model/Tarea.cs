using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public partial class Tarea
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool State { get; set; }

    public virtual ICollection<Fecha> Fechas { get; } = new List<Fecha>();
}
