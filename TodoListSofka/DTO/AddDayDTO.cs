using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class AddDayDTO
    {
        [Required] public int NumberDay { get; set; }
        [Required] public string Name { get; set; } = null!;
    }
}
