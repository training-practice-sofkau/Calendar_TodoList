using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.DTO;
using TodoListSofka.Models;

namespace TodoListSofka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoitemController : ControllerBase
    {
        private readonly CalendardbContext _CalendardbContext;

        public TodoitemController(CalendardbContext calendardbContext)
        {
            _CalendardbContext = calendardbContext;
        }



    }
}
