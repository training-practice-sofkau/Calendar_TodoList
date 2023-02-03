using System.ComponentModel.DataAnnotations;
using TodoListSofka.DTO.ToDoItem;

namespace TodoListSofka.DTO.Calendar
{
    public class GetCalendarDTO
    {
        [Required]
        public int IndexDay { get; set; }
    }
}
