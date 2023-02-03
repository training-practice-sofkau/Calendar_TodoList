using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoListSofka.DTO;
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

        //Metodo post que agrega un evento a un día específico, de no existir se crea ese día en el calendario
        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemDTO dto)
        {
            try
            {
                var item = await _context.Calendars.Where(x => x.Name == dto.NameCalendar).Include(r => r.Days).ToListAsync();
                if (!item.IsNullOrEmpty())
                {

                    if (dto.NumberDay > 0 && dto.NumberDay < 29)
                    {
                        var days = item.First().Days.Where(x => x.NumberDay == dto.NumberDay);
                        if (days.IsNullOrEmpty())
                        {
                            Day day = new Day { NumberDay = dto.NumberDay, IdCalendar = item.First().Id };
                            await _context.AddAsync(day);
                            await _context.SaveChangesAsync();
                            Todoitem result = _mapper.Map<Todoitem>(dto);
                            result.IdDay = day.Id;
                            await _context.AddAsync(result);
                            await _context.SaveChangesAsync();
                            return Ok(new { code = 200, message = $"El Evento {result.Title} se agregó al día {dto.NumberDay} del calendario {dto.NameCalendar} con éxito" });
                        }
                        else
                        {
                            Todoitem result = _mapper.Map<Todoitem>(dto);
                            result.IdDay = days.First().Id;
                            await _context.AddAsync(result);
                            await _context.SaveChangesAsync();
                            return Ok(new { code = 200, message = $"El Evento {result.Title} se agregó al día {dto.NumberDay} del calendario {dto.NameCalendar} con éxito" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { code = 400, message = $"El día {dto.NumberDay} no es un número valido para agregar en el calendario" });
                    }
                }
                else
                {
                    return NotFound(new { code = 404, message = "No existe un calendario con ese nombre" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 500, message = $"No se puede mostrar el item: {ex.Message}" });
            }
        }

        //Get por id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id)
        {
            try
            {
                var item = await _context.Todoitems.Where(x => x.Id == id && x.State && !x.IsCompleted).ToListAsync();
                if (item.IsNullOrEmpty())
                {
                    return BadRequest(new { code = 404, message = "No hay items para mostrar con ese id" });
                }

                return Ok(item[0]);
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 500, message = $"No se puede mostrar el item: {e.Message}" });
            }
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid id, TodoitemDTO dto)
        {
            try
            {
                var item = await _context.Todoitems.Where(x => x.Id == id && x.State).ToListAsync();
                if (!item.IsNullOrEmpty())
                {
                    _mapper.Map(dto, item[0]);
                    await _context.SaveChangesAsync();
                    return Ok(item[0]);
                }

                return BadRequest(new { code = 404, message = "No hay items para actualizar con ese id" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 500, message = $"No se puede mostrar el item: {e.Message}" });
            }

        }

    }
}
