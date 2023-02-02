using TodoListSofka.Model;

namespace TodoListSofka.DTO.Day
{
    public class TodoCreateDayDTO
    {
        public ToDoCreateDTO ToDoCreateDTO { get; set; }

        public List<TodoItem> todoItems { get; set; }
    }
}
