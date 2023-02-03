using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Models;

public partial class Calendar
{
    [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
    public int NumberDaY { get; set; }
    [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
    public  ICollection<Item> Items { get;} = new List<Item>();


}
