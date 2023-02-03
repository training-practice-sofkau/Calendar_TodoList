using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Models;

namespace TodoListSofka.Controllers
{
    public class JornadaMorning : IJornada
    {
       
        public Object ShowJornada(Item taskCompleted){
            
            string jornada = "mañana";
            Day daycomplete = new Day(taskCompleted.Title, taskCompleted.IdCalendar, jornada);

            return daycomplete;
        }
    }
}
