using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoListSofka.Dto;
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
        public async Task<IActionResult> GetCalendars()
        {
            try
            {

                //var activeRecords =  _dbContext.Calendars.Where(r => r.Items.FirstOrDefault().Estate!=0).ToList();
                // List<Calendar> activeRecords = _dbContext.Calendars.
                var activeRecords = await  _dbContext.Calendars.Include(r => r.Items).ToListAsync();

                return Ok(activeRecords);
            }
            catch (Exception)
            {

                throw;
            }


        }



        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetCalendars()
        {
            try
            {

                //var activeRecords =  _dbContext.Calendars.Where(r => r.Items.FirstOrDefault().Estate!=0).ToList();
                // List<Calendar> activeRecords = _dbContext.Calendars.
                var activeRecords = _dbContext.Calendars.ToList();

                return activeRecords;
            }
            catch (Exception)
            {

                throw;
            }


        }
        */

        [HttpPost]
        public async Task<ActionResult> PostItem(ItemAgregar item)
        {

            var itemcreado = new Item()
            {
                Title = item.Title,
                Descripccion = item.Descripccion,
                Resposible = item.Resposible,
                IsCompleted = item.IsCompleted,
                Estate = 1,
                IdCalendar =item.IdCalendar

            };

            var calendario = new Calendar()
            {
                NumberDaY = item.IdCalendar

            };


            await _dbContext.Items.AddAsync(itemcreado);
            calendario.Items.Add(itemcreado);
            await _dbContext.Calendars.AddAsync(calendario);
            await _dbContext.SaveChangesAsync();

            return Ok($"Tarea creada con exito {itemcreado.ToString()}");
        }

    }
}
