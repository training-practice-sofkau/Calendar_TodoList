using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class TodoitemActualizar
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
