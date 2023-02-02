using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public partial class Todoitem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Responsible { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public bool State { get; set; }

    public Guid IdDay { get; set; }

    public virtual Day IdDayNavigation { get; set; } = null!;
}
