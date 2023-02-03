using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoitemController : Controller
    {
        private CalendardbContext _context;
        private readonly IMapper _mapper;


        public TodoitemController(CalendardbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Metodo que retorna la lista de los items activos y no completos
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var item = await _context.Todoitems.Where(x => x.State && !x.IsCompleted).ToListAsync();
                if (item.IsNullOrEmpty())
                {
                    return BadRequest(new { code = 404, message = "No hay items para mostrar" });
                }
                return Ok(item);
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 500, message = $"No se puede listar: {e.Message}" });
            }
        }

    }
}
