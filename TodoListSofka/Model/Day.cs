using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public partial class Day
{
    public Guid Id { get; set; }

    public int NumberDay { get; set; }

    public Guid IdCalendar { get; set; }

    public virtual Calendar IdCalendarNavigation { get; set; } = null!;

    public virtual ICollection<Todoitem> Todoitems { get; } = new List<Todoitem>();
}
