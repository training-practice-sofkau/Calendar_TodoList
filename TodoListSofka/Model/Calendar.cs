using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public partial class Calendar
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Day> Days { get; } = new List<Day>();
}
