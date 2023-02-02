using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Models;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CalendarController : ControllerBase
    {
        private readonly CalendardbContext _CalendardbContext;

        public CalendarController(CalendardbContext calendardbContext)
        {
            _CalendardbContext = calendardbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetCalendar()
        {
            var item = await _CalendardbContext.Calendars.ToListAsync();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Calendar>> PostCalendar()
        {
            _CalendardbContext.Calendars.Add(new Calendar());
            await _CalendardbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
