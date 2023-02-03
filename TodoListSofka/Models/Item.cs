using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Models;

public partial class Item
{
    [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
    public string Title { get; set; } = null!;
    public string Descripccion { get; set; } = null!;
    [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
    public string Resposible { get; set; } = null!;
    [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
    public bool IsCompleted { get; set; }
    [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
    public int Estate { get; set; }
    [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
    public int? IdCalendar { get; set; }
    [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
    public virtual Calendar? IdCalendarNavigation { get; set; }

    public override string? ToString()
    {

     return $" --Tarea: {Title} , tiene como objetivo: {Descripccion},  la persona encargada es: {Resposible}, su estado actual es: {Estate} y el día al cual fue asignado es: {IdCalendar} de febrero";
    }
}
