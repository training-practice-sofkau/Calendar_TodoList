using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
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
