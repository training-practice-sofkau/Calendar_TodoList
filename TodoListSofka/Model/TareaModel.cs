using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace TodoListSofka.Model;

public partial class TareaModel
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool State { get; set; }

    public virtual ICollection<FechaModel> Fechas { get; } = new List<FechaModel>();
    public TareaModel(Guid id, String nombre, String descripcion, bool state)
    {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
        State = state;
    }
    public TareaModel() { }

}
