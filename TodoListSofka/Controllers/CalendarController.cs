using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoListSofka.DTO;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {

        private readonly CalendardbContext _context;
        private readonly IMapper _mapper;

        public CalendarController(CalendardbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Calendars

        [HttpGet]
        [Route("getCalendars/")]
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

        //POST: Calendar
        [HttpPost]
        public async Task<IActionResult> AddCalendar(CalendarDTO dto)
        {
            Calendar calendar = _mapper.Map<Calendar>(dto);
            await _context.AddAsync(calendar);
            await _context.SaveChangesAsync();

            return Ok(calendar);
        }

        //Get por id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCalendar([FromRoute] Guid id)
        {
            try
            {
                var item = await _context.Calendars.Where(x => x.Id == id && !x.IsDeleted).ToListAsync();
                if (item.IsNullOrEmpty())
                {
                    return BadRequest(new { code = 404, message = "No hay calendarios para mostrar con ese id" });
                }

                return Ok(item.First());
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 500, message = $"No se puede mostrar el calendario: {e.Message}" });
            }
        }

        //Eliminado Logico 
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCalendar([FromRoute] Guid id)
        {
            try
            {
                var item = await _context.Calendars.FindAsync(id); //Agregar filtro LINQ
                if (item != null)
                {
                    item.IsDeleted = true;
                    await _context.SaveChangesAsync();
                    return Ok(item);
                }

                return BadRequest(new { code = 404, message = "No hay calendarios para eliminar con ese id" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 500, message = $"No se puede eliminar el calendario: {e.Message}" });
            }
        }

    }
}
