using System;
using System.Collections.Generic;

namespace TodoListSofka.Models;

public partial class Item
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Descripccion { get; set; } = null!;

    public string Resposible { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public int Estate { get; set; }

    public int? IdCalendar { get; set; }

    public virtual Calendar? IdCalendarNavigation { get; set; }

    public override string? ToString()
    {

     return $" --Tarea: {Title} , tiene como objetivo: {Descripccion},  la persona encargada es: {Resposible}, su estado actual es: {Estate} y el día al cual fue asignado es: {IdCalendar} de febrero";
    }
}
