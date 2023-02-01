using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }

        //para el borrado lógico implementar bool o int según su preferencia.
        //si es int puedo colcocar un rango entre o y 1
        public bool State { get; set; }

        public TodoItem(Guid id, DateTime fecha, int dia, int mes,
            int año, bool state)
        {
            Id = id;
            Fecha = fecha;
            Dia = dia;
            Mes = mes;
            Año = año;
            State = state;
        }

        public TodoItem() { }
    }
}
