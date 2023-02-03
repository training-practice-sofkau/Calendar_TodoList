using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model;

public partial class Calendar
{
    public Guid Id { get; set; }

    [Required] public string Name { get; set; } = null!;

    [Required] public string Description { get; set; } = null!;

    [Required] public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Day> Days { get; } = new List<Day>();
}
