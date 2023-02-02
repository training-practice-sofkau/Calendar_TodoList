namespace TodoListSofka.Model
{
    public class Day : Calendar
    {
        public List<TodoItem> todoItems = new List<TodoItem>();
       
        public DateTime dateTime { get; set; }
        public Day()
        {
        }

    }
}
