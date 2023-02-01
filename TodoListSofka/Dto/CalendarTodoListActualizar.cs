using TodoListSofka.Models;

namespace TodoListSofka.Dto
{
    public class CalendarTodoListActualizar
    {

       public int NumberDaY { get; set; }

        public virtual ICollection<Item> Items { get; } = new List<Item>();


    }
}
