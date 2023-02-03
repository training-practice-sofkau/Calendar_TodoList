using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoListSofka.Data;
using TodoListSofka.Model;
using TodoListSofka.PatternDesign;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : Controller
    {
        readonly CalendarToDoContext _dbContext;
        User users = new();
        public CalendarController(CalendarToDoContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Se traen todos los dias
        [HttpGet("AllDays")]
        public async Task<IActionResult> GetAllDays()
        {
            try
            {
                var days = _dbContext.Calendars.
                    Include(list => list.ToDoItems.
                    Where(item => item.State));

                if (!days.IsNullOrEmpty())
                {
                    return Ok(days);
                }
                return BadRequest(new { code = 404, message = "No hay dias para mostrar" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay dias para mostrar: {e.Message}" });
            }
        }

        //Se trae un dia
        [HttpGet("Calendar/{id:int}")]
        public async Task<IActionResult> GetDay([FromRoute] int id)
        {
            try
            {
                var days = _dbContext.Calendars.
                    Include(list => list.ToDoItems.
                    Where(item => item.State)).
                    Where(c => c.IndexDay == id);

                if (!days.IsNullOrEmpty())
                {
                    return Ok(await days.ToListAsync());
                }
                return BadRequest(new { code = 404, message = "No hay un día con este id" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay dias para mostrar: {e.Message}" });
            }
        }

        //Añadir Dia
        [HttpPost("{day:int}")]
        public async Task<IActionResult> AddDay([FromRoute] int day)
        {
            try
            {
                User Kevin = new(new Guid(), "Kevin");
                User Santiago = new(new Guid(), "Santiago");
                User Random = new(new Guid(), "Random");

                Calendar createdDay = new(day);

                Kevin.Subscribe(createdDay);
                Santiago.Subscribe(createdDay);
                Random.Subscribe(createdDay);

                await _dbContext.Calendars.AddAsync(createdDay);
                await _dbContext.SaveChangesAsync();

                Random.Unsubscribe(createdDay);
                users.Update(createdDay);
                return Ok($"Se creó el día: {createdDay.IndexDay}");
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo añadir el elemento: {e.Message}" });
            }
        }
    }
}
