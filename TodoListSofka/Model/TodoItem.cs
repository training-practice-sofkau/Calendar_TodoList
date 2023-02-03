using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model;

public partial class ToDoItem
{
    public Guid ItemId { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string Responsible { get; set; } = null!;
    [Required]
    public bool IsCompleted { get; set; }
    [Required]
    public bool State { get; set; }
    [Required]
    [Range(1, 28,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int? IndexDay { get; set; }

    public virtual Calendar? IndexDayNavigation { get; set; }
}
