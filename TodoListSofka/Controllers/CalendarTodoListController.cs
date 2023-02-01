using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListSofka.Models;

namespace TodoListSofka.Controllers
{
    [Route("api/Controlador")]
    [ApiController]
    public class CalendarTodoListController : ControllerBase
    {


        private readonly CalendarTodoListContext _dbContext;


        public CalendarTodoListController(CalendarTodoListContext dbContext){

            _dbContext = dbContext;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetCalendars()
        {
            try
            {


                // var activeRecords = _dbContext.TodoItems.Where(r => r.Estate != 0).ToList();

                var activeRecords = _dbContext.Calendars.ToList();                
                return activeRecords;
            }
            catch (Exception)
            {

                throw;
            }


        }








    }
}
