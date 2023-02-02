using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class GetTareaDTO
    {
        [Required]
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
