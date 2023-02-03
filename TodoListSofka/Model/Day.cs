using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model;

public partial class Day
{
    public Guid Id { get; set; }

    [Required] public int NumberDay { get; set; }

    [Required] public Guid IdCalendar { get; set; }

    [Required] public bool IsDeleted { get; set; } = false;

    public virtual Calendar IdCalendarNavigation { get; set; } = null!;

    public virtual ICollection<Todoitem> Todoitems { get; } = new List<Todoitem>();
}
