using Microsoft.AspNetCore.Mvc;
using TodoListSofka.Models;

namespace TodoListSofka.Controllers
{
    public class JornadaNigth : IJornada
    {
        public Object ShowJornada(Item taskCompleted)
        {
             
            string jornada = "Noche";
            Day daycomplete = new Day(taskCompleted.Title, taskCompleted.IdCalendar, jornada);

            return daycomplete;
        }
    }
}
