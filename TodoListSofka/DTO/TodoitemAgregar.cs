using System.ComponentModel.DataAnnotations;
using TodoListSofka.Model;

namespace TodoListSofka.DTO
{
    public class TodoitemAgregar : CalendarioDTO
    {
        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public string Responsible { get; set; } = null!;

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public bool IsComplete { get; set; }
    }
}
