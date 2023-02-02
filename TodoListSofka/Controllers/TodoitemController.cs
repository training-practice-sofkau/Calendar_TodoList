using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todoitem>>> GetTodoitems()
        {
            try
            {
                var item = await _CalendardbContext.Todoitems.Where(x => x.State == true && x.IsCompleted == false).ToListAsync();
                return item;
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 404,
                    message = $"No hay items para mostrar: {e.Message}"
                });
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<IEnumerable<Todoitem>>> GetTodoitems([FromRoute] Guid id)
        {
            try
            {
                var item = _CalendardbContext.Todoitems.Where(x =>
                    x.IdCalendar == id || x.Id == id && x.State == true && x.IsCompleted == false).ToList();
                if (item.IsNullOrEmpty())
                {
                    return BadRequest(new
                    { code = 404, message = $"No existen o no están disponibles items con el id asignado" });
                }
                return item;
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = e.Message });
            }
        }

        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> CreateTodoItem(Guid id, TodoitemDTO todoItemDTO)
        {

            try
            {
                var todoItem = new Todoitem
                {
                    IdCalendar = id,
                    Title = todoItemDTO.Title,
                    Description = todoItemDTO.Description,
                    Responsible = todoItemDTO.Responsible
                };

                await _CalendardbContext.Todoitems.AddAsync(todoItem);
                await _CalendardbContext.SaveChangesAsync();

                return Ok(todoItem);
            }
            catch (Exception e)
            {
                return BadRequest(new
                { code = 404, message = $"No existen días con el id asignado: {e.Message}" });
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodoItem(Guid id, TodoitemDTO todoItemDTO)
        {
            var todoItem = await _CalendardbContext.Todoitems.FindAsync(id);

            if (todoItem == null)
            {
                return BadRequest(new
                { code = 404, message = $"No existen o no están disponibles items con el id asignado" });
            }

            todoItem.Title = todoItemDTO.Title;
            todoItem.Description = todoItemDTO.Description;
            todoItem.Responsible = todoItemDTO.Responsible;

            await _CalendardbContext.SaveChangesAsync();

            return Ok(todoItem);
        }

        [HttpPut]
        [Route("update/{id:Guid}")]
        public async Task<IActionResult> CompleteTodoItem(Guid id)
        {
            var todoItem = await _CalendardbContext.Todoitems.FindAsync(id);

            if (todoItem == null)
            {
                return BadRequest(new
                { code = 404, message = $"No existen o no están disponibles items con el id asignado" });
            }

            todoItem.IsCompleted = true;

            await _CalendardbContext.SaveChangesAsync();

            return Ok(todoItem);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            var todoItem = await _CalendardbContext.Todoitems.FindAsync(id);

            if (todoItem == null)
            {
                return BadRequest(new
                { code = 404, message = $"No existen o no están disponibles items con el id asignado" });
            }

            todoItem.State = false;

            await _CalendardbContext.SaveChangesAsync();

            return Ok(todoItem);
        }

    }
}
