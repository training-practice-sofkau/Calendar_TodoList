using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class GetFechaDTO
    {
        [Required]
        public DateTime Fecha { get; set; }
        public int? Dia { get; set; }
        public int? Mes { get; set; }
        public int? Año { get; set; }
        public bool State { get; set; }
    }
}
