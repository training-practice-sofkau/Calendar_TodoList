using System;
using System.Collections.Generic;

namespace TodoListSofka.Model;

public partial class FechaModel
{
    public Guid Id { get; set; }

    public DateTime Fecha { get; set; }

    public int? Dia { get; set; }

    public int? Mes { get; set; }

    public int? Año { get; set; }

    public bool State { get; set; }

    public Guid IdEventos { get; set; }
    public FechaModel(Guid id, DateTime fecha, int dia, int mes,
            int año, bool state, Guid idEventos)
    {
        Id = id;
        Fecha = fecha;
        Dia = dia;
        Mes = mes;
        Año = año;
        State = state;
        IdEventos = idEventos;
    }
    public virtual TareaModel IdEventosNavigation { get; set; } = null!;
    public FechaModel() { }
}
