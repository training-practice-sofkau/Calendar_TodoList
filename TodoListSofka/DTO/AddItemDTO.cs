using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class AddItemDTO
    {
        [Required] public string Title { get; set; } = null!;

        [Required] public string Description { get; set; } = null!;

        [Required] public string Responsible { get; set; } = null!;

        [Required] public int NumberDay { get; set; }

        [Required] public string NameCalendar { get; set; } = null!;
    }
}
