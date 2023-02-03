using System;
using System.Collections.Generic;

namespace TodoListSofka.Models;

public partial class Calendar
{
    public Guid Id { get; set; }

    public int Day { get; set; }

    public virtual ICollection<Todoitem> Todoitems { get; } = new List<Todoitem>();
}
