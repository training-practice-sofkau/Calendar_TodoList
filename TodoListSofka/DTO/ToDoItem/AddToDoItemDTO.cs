using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO.ToDoItem
{
    public class AddToDoItemDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Responsible { get; set; } = null!;
    }
}
