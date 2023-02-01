using System;
using System.Collections.Generic;

namespace TodoListSofka.Models;

public partial class Calendar
{
    public int NumberDaY { get; set; }

    public virtual ICollection<Item> Items { get; } = new List<Item>();
}
