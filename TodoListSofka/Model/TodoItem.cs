using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public partial class Todoitem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Responsible { get; set; } = null!;

    public bool IsComplete { get; set; }

    public int State { get; set; }

    public Guid IdCalendar { get; set; }

    public virtual Calendario IdCalendarNavigation { get; set; } = null!;
}
