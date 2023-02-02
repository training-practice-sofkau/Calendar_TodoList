using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Data;
using TodoListSofka.DTO;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {

        private readonly ToDoAPIDbContext dbContext;

        public ToDoController(ToDoAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonajes()
        {
            //Busca las Tareas que no hayan sido eliminados y los retorna
            var tareaActiva = dbContext.ListaTareas1.Where(r => r.State != false).ToList();
            return Ok(tareaActiva);

            //Muestra todos los personajes 
            //return await dbContext.Tareas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var tarea = await dbContext.ListaTareas1.Where(r => r.State != false && r.Id == id).ToListAsync();
                if (tarea == null || id == 0 || tarea.Count == 0)
                {
                    return BadRequest(new { code = 400, message = "Id no encontrado. " });
                }
                else
                {
                    return Ok(tarea);
                }
            }
            catch (Exception ex)
            {
                return NotFound(new { code = 404, message = $"Id no encontrado. : {ex.Message}" });
            }
        }

        [HttpGet("/Prioridad")]
        public async Task<Object> GetPriority(string importancia)
        {
            var tareaImportante = dbContext.ListaTareas1.Where(r => r.Priority == importancia && r.State != false).ToList();
            return tareaImportante;
        }

        [HttpPost]
        public async Task<Object> Post(ToDoCreateDTO tareaDto)
        {
            var nuevaTarea = new TodoItem();
            nuevaTarea.Title = tareaDto.Title;
            nuevaTarea.Description = tareaDto.Description;
            nuevaTarea.Responsible = tareaDto.Responsible;
            nuevaTarea.Priority = tareaDto.Priority;
            nuevaTarea.IsCompleted = tareaDto.IsCompleted;
            nuevaTarea.State = true;

            dbContext.Add(nuevaTarea);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("/FechaEvento")]
        public async Task<Object> Post(Day fechaDto)
        {
            var nuevafecha = new Day();
            nuevafecha.dateTime = fechaDto.dateTime;
            nuevafecha.todoItems = fechaDto.todoItems;
            dbContext.Add(nuevafecha);
            
            await dbContext.SaveChangesAsync();
            return Ok();
            
        }

        [HttpPut]
        public async Task<Object> Put(ToDoUpdateDto itemData)
        {
            if (itemData == null || itemData.Id == 0)
                return BadRequest("El ID no es correcto. ");

            var tarea = await dbContext.ListaTareas1.FindAsync(itemData.Id);
            if (tarea == null)
                return NotFound("La tarea no existe. ");
            if (tarea.State == false)
                return NotFound("la tarea se ha sido eliminado. ");
            tarea.Title = itemData.Title;
            tarea.Description = itemData.Description;
            tarea.Responsible = itemData.Responsible;
            tarea.Priority = itemData.Priority;
            tarea.IsCompleted = itemData.IsCompleted;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound(new { code = 404, message = $"Id no encontrado. : {ex.Message}" });
            }
            return Ok();

        }

        [HttpPut("/Estado/{id:int}")]
        public async Task<Object> PutEstado(int id, bool estado)
        {
            var tarea = await dbContext.ListaTareas1.FindAsync(id);
            tarea.IsCompleted = estado;
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<Object> Delete(int id)
        {
            var tarea = await dbContext.ListaTareas1.FindAsync(id);
            if (tarea.State == false) return NotFound("El personaje ya ha sido eliminado. ");
            if (tarea == null) return NotFound("ID incorrecto");
            dbContext.ListaTareas1.Remove(tarea);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
