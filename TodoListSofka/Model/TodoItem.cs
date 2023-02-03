using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model;

public partial class Todoitem
{
    public Guid Id { get; set; }

    [Required] public string Title { get; set; } = null!;

    [Required] public string Description { get; set; } = null!;

    [Required] public string Responsible { get; set; } = null!;

    [Required] public bool IsCompleted { get; set; } = false;

    [Required] public bool State { get; set; } = true; 

    [Required] public Guid IdDay { get; set; }

    public virtual Day IdDayNavigation { get; set; } = null!;
}
