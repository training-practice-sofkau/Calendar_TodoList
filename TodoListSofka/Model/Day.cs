namespace TodoListSofka.Model
{
    public class Day 
    {
        public List<TodoItem> todoItems = new List<TodoItem>();
       
        public DateTime dateTime { get; set; }
        public Day()
        {
        }

    }
}
