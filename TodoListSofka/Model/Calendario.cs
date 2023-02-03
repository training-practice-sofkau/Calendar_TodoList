using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public partial class Calendario
{
    public Guid Id { get; set; }

    public int Dia { get; set; }

    public int Mes { get; set; }

    public int Anio { get; set; }

    public int Jornada { get; set; }

    public virtual ICollection<Todoitem> Todoitems { get; } = new List<Todoitem>();
}
