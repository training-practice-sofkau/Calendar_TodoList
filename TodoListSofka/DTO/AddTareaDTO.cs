using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class AddTareaDTO
    {
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        [Required]
        public string Jornada { get; set; } = null!;
    }
}
