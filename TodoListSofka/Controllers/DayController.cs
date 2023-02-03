using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayController : Controller
    {
        private readonly CalendardbContext _context;
        private readonly IMapper _mapper;

        public DayController(CalendardbContext context, IMapper mapper)
        {
            _context= context;
            _mapper= mapper;
        }

        [HttpGet]
        [Route("getDays/")]
        public async Task<IActionResult> GetDays()
        {
            try
            {
                var item = await _context.Days.Where(x=> !x.IsDeleted).ToListAsync();
                if(item.IsNullOrEmpty())
                {
                    return NotFound(new { code = 404, message = "No hay items para mostrar" });
                }
                
                return Ok(item);

            }catch(Exception ex)
            {
                return BadRequest(new { code = 500, message = $"No se puede listar: {ex.Message}" });
            }
        }
        
    }
}
