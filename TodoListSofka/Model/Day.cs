namespace TodoListSofka.Model
{
    public class Day
    {       
        public int Id { get; set; }
        public string dayNumber { get; set; }

        public string dayWeek { get; set; }

        public Day()
        {
        }

        public Day(string dayNumber, string dayWeek)
        {
            this.dayNumber = dayNumber;
            this.dayWeek = dayWeek;
        }

    }
    

}
