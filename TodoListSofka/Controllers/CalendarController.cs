using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {

        private readonly CalendardbContext _context;

        public CalendarController(CalendardbContext context)
        {
            _context = context;
        }

        // GET: Calendars

        [HttpGet]
        public async Task<IActionResult> GetCalendars()
        {
            try
            {
                var item = await _context.Calendars.Where(x => !x.IsDeleted).ToListAsync();
                if(item.IsNullOrEmpty())
                {
                    return NotFound(new {code = 404, message = "No hay items para mostrar"});
                }
                return Ok(item);
            }
            catch(Exception ex)
            {
                return BadRequest(new { code = 500, message = $"No se puede listar: {ex.Message}" });
            }
        }

    }
}
