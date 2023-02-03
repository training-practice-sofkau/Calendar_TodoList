using System.ComponentModel.DataAnnotations;
using TodoListSofka.PatternDesign;

namespace TodoListSofka.Model;

public partial class Calendar
{
    public List<User> _usersList;
    [Required]
    [Range(1, 28,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int IndexDay { get; set; }
    public virtual ICollection<ToDoItem> ToDoItems { get; } = new List<ToDoItem>();
    public Calendar(int indexDay)
    {
        IndexDay = indexDay;
        _usersList = new List<User>();
    }

}
