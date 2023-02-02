using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Route("AllDaysAndItems")]
        public async Task<IActionResult> GetCalendars()
        {
            try
            {

                //var activeRecords =  _dbContext.Calendars.Where(r => r.Items.FirstOrDefault().Estate!=0).ToList();
                // List<Calendar> activeRecords = _dbContext.Calendars.
                var activeRecords = await _dbContext.Calendars.Include(r => r.Items.Where(x => x.Estate != 0)).ToListAsync();
                //.Where(y=> y.Items!=null)
                return Ok(activeRecords);
            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpGet]
        [Route("/OneDayAndOneItem/{id:int}")]
        public async Task<IActionResult> GetDayAndItem([FromRoute] int id)
        {
            try
            {
                
                var task = from aux in _dbContext.Items where (aux.IdCalendar == id && aux.Estate!=0) select aux;
                
               
                return Ok(task);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPut]
        [Route("/CompletedOneTask/{id:int}")]
        public async Task<IActionResult> CompleteOneItem([FromRoute] int id, bool complete)
        {
            try
            {


                var respon = await _dbContext.Items.FindAsync(id);
                respon.IsCompleted = complete;
                await _dbContext.SaveChangesAsync();

                return Ok(respon);

            }

            catch (DbUpdateConcurrencyException)
            {




            }

             return Ok("Tarea actualizada");
        }


        [HttpPut]
        [Route("/UpdateAllTask/{id:int}")]
        public async Task<IActionResult> updateTaks([FromRoute] int id, ItemActualizar todoitemAc)
        {
            try
            {

                var item = await _dbContext.Items.FindAsync(id);
                item.Title = todoitemAc.Title;
                item.Descripccion = todoitemAc.Descripccion;
                item.Resposible = todoitemAc.Resposible;
                item.IsCompleted = todoitemAc.IsCompleted;
                item.IdCalendar = todoitemAc.IdCalendar;

                await _dbContext.SaveChangesAsync();
                return Ok($"Tarea actualizada, {item}");

            }

            catch (DbUpdateConcurrencyException)
            {

            }

            return Ok("Tarea actualizada");
        }


        [HttpPost]
        public async Task<ActionResult> PostItem(ItemAgregar item, int dayUser)
        {

            var day = await _dbContext.Calendars.FindAsync(dayUser);


            if (day == null){

                var calendario = new Calendar()
                {

                    NumberDaY = dayUser
                    
                 };


                var itemcreado = new Item()
                {
                    Title = item.Title,
                    Descripccion = item.Descripccion,
                    Resposible = item.Resposible,
                    IsCompleted = item.IsCompleted,
                    Estate = 1,
                    IdCalendar = dayUser

                };
                await _dbContext.Items.AddAsync(itemcreado);
                calendario.Items.Add(itemcreado);
                await _dbContext.Calendars.AddAsync(calendario);
                await _dbContext.SaveChangesAsync();
            }

            var itemcreado2 = new Item()
            {
                Title = item.Title,
                Descripccion = item.Descripccion,
                Resposible = item.Resposible,
                IsCompleted = item.IsCompleted,
                Estate = 1,
                IdCalendar = dayUser
            };

            await _dbContext.Items.AddAsync(itemcreado2);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {

           // var item = await _dbContext.TodoItems.FindAsync(id);
            var recordToUpdate = _dbContext.Items.FirstOrDefault(r => r.Id == id);

            try
            {
                if (recordToUpdate != null){
                    recordToUpdate.Estate = 0;
                    _dbContext.SaveChanges();
                }
                else { return NotFound(); }

                return Ok(new
                {
                    code = 200,
                    message = $"La tarea con id {id} fue eliminada"
                }
                );
            }
            catch (Exception)
            {
                throw;
            }


        }


        private bool ItemAvailable(int id)
        {

            return (_dbContext.Items?.Any(x => x.Id == id)).GetValueOrDefault();

        }

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