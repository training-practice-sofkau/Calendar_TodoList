using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class AddFechaDTO
    {
        [Required]
        public DateTime Fecha { get; set; }
    }
}
