using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class CalendarDTO
    {
        [Required] public string Name { get; set; } = null!;

        [Required] public string Description { get; set; } = null!;
    }
}
