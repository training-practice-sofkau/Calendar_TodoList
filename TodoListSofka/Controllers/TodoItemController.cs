using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.DTO;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly TodoListContext _dbContext;

        public TodoItemController(TodoListContext dbContext)
        {

            _dbContext = dbContext;

        }

        /// <summary>
        /// Metodo Listar Todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/listar")]
        public async Task<ActionResult> GetItems()
        {
            try
            {
                var activeRecords = _dbContext.Todoitems.Where(r => r.State != 0).ToList();
                return Ok(activeRecords);
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        /// <summary>
        /// Metodo Listar Uno
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/listaruno/{id:guid}")]
        public async Task<ActionResult> GetItem([FromRoute] Guid id)
        {
            try
            {
                var item = await _dbContext.Todoitems.FindAsync(id);
                // var result = _dbContext.TodoItems.Where(r => r.Estate != 0).ToList();


                if (item.State == 0)
                {

                    return NotFound(new{message = "La tarea con ese id no existe"});

                }

                return Ok(item);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Metodo Guardar
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/guardar")]
        public async Task<ActionResult> PostItem(TodoitemAgregar item)
        {
            var fecha = new Calendario()
            {
                Id = Guid.NewGuid(),
                Dia = item.Dia,
                Mes = item.Mes,
                Anio = item.Anio,
                Jornada = item.Jornada
            };

            var items = new Todoitem()
            {
                Title = item.Title,
                Description = item.Description,
                Responsible = item.Responsible,
                IsComplete = item.IsComplete,
                State = 1,
                IdCalendar = fecha.Id
            };

            await _dbContext.Calendarios.AddAsync(fecha);
            await _dbContext.Todoitems.AddAsync(items);
        
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Actualizar Todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoitemAc"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/actualizar/{id:guid}")]
        public async Task<IActionResult> ActualizarItem([FromRoute] Guid id, TodoitemActualizar todoitemAc)
        {
            var item = await _dbContext.Todoitems.FindAsync(id);

            if (item != null && item.State != 0)
            {

                item.Title = todoitemAc.Title;
                item.Description = todoitemAc.Description;
                item.Responsible = todoitemAc.Responsible;
                item.Description = todoitemAc.Description;

                await _dbContext.SaveChangesAsync();

            }
            else {
                return NotFound();
            }
            return Ok("La tarea se ha actualizado de forma correcta!");
        }

        /// <summary>
        /// Actualizar Uno
        /// </summary>
        /// <param name="id"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/complete/{id:guid}")]
        public async Task<IActionResult> ActualizarOneItem([FromRoute] Guid id, bool complete)
        {
            //var result = _dbContext.TodoItems.Where(r => r.Estate == 0).ToList();

            try
            {
                var respon = await _dbContext.Todoitems.FindAsync(id);
                respon.IsComplete = complete;
                await _dbContext.SaveChangesAsync();

                return Ok("La tarea se ha editado de forma correcta!");
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ItemAvailable(id)){

                   return NotFound("Problemas en la actualizacion de la base de datos");

                }
                else{throw;}

            }

        }

        /// <summary>
        /// Borrado Lógico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/eliminar/{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {

            var item = await _dbContext.Todoitems.FindAsync(id);
            var recordToUpdate = _dbContext.Todoitems.FirstOrDefault(r => r.Id == id);

            try
            {
                if (recordToUpdate != null)
                {
                    recordToUpdate.State = 0;
                    _dbContext.SaveChanges();
                }
                else { return NotFound(); }


                return Ok(new {code = 200, message = $"El usuario con id {id} fue eliminado"});
            }
            catch (Exception)
            {
                throw;
            }

        }

        private bool ItemAvailable(Guid id)
        {

            return (_dbContext.Todoitems?.Any(x => x.Id == id)).GetValueOrDefault();

        }


    
    }
}
