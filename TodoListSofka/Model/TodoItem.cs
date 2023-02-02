using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model;

public partial class Todoitem
{

    public Guid Id { get; set; }

    [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
    public string Responsible { get; set; } = null!;

    [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
    public bool IsComplete { get; set; }

    public int State { get; set; }
}
