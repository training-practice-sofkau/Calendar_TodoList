﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoListSofka.DTO;
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

        //Metodo que lista los días de un calendario
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


        //Metodo que agrega un día a un calendario basandose en el mes de febrero, no debería guardar días mayores a 28 ni repetidos
        [HttpPost]
        public async Task<IActionResult> AddDay(AddDayDTO dto)
        {
            try
            {
                var item = await _context.Calendars.Where(x => x.Name == dto.Name).Include(r => r.Days).ToListAsync();
                if(!item.IsNullOrEmpty())
                {

                    if(dto.NumberDay > 0 && dto.NumberDay < 29)
                    {
                        var days = item.First().Days.Where(x => x.NumberDay == dto.NumberDay);
                        if (days.IsNullOrEmpty())
                        {
                            Day day = new Day { NumberDay = dto.NumberDay, IdCalendar = item.First().Id };
                            await _context.AddAsync(day);
                            await _context.SaveChangesAsync();
                            return Ok(new { code = 200, message = $"El Día {day.NumberDay} se agregó a {dto.Name} con éxito" });
                        }
                        else
                        {
                            return BadRequest(new { code = 400, message = $"El día {dto.NumberDay} ya existe en el mes de {dto.Name}" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { code = 400, message = $"El día {dto.NumberDay} no es un número valido para agregar en el calendario"});
                    }
                }
                else
                {
                    return NotFound(new { code = 404, message = "No existe un calendario con ese nombre" });
                }

            }catch(Exception ex)
            {
                return BadRequest(new { code = 500, message = $"No se puede mostrar el item: {ex.Message}" });
            }
        }
    }
}