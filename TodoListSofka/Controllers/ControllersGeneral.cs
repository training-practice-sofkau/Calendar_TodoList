using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Data;
using TodoListSofka.Model;
using TodoListSofka.DTO;
using AutoMapper;
using TodoListSofka.PatternDesign;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllersGeneral : Controller
    {
        //creamos una variable del contexto
        private readonly CalendarEventoContext dbContext;
        private readonly IMapper _mapper;

        public ControllersGeneral(CalendarEventoContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        //Saco todos las fechas
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Fechas = await dbContext.Fechas.Where(list => list.State).ToListAsync();
                var Tarea = await dbContext.Tareas.Where(list => list.State).ToListAsync();

                //Creamos la instancia del creador de listas (Singleton Pattern)
                CreatorList listMapped = CreatorList.GetInstance();
                if (Fechas.Count != 0 && Fechas != null && Tarea.Count != 0 && Tarea != null)
                {
                    listMapped.ListItems.Clear();
                    foreach (var item in Fechas)
                    {
                        listMapped.ListItems.Add(_mapper.Map<GetFechaDTO>(item));
                    }
                    return Ok(listMapped);
                }
                return BadRequest(new { code = 404, message = "No hay elementos para mostrar" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay elementos para mostrar: {e.Message}" });
            }
        }

        //Se traen todas las fechas en state false
        [HttpGet("UncompleteItems/Get")]
        public async Task<IActionResult> GetUncompleteFecha()
        {
            try
            {
                //consulta a la db mediante linq + DTO para get de elementos sin completar

                var Fechas = await dbContext.Fechas.Where(list => list.State && !list.State)
                    .ToListAsync();

                //Creamos la instancia del creador de listas (Singleton Pattern)
                CreatorList listMapped = CreatorList.GetInstance();

                if (Fechas.Count != 0 && Fechas != null)
                {
                    //vaciar la lista cada vez que "cree" la instancia para usarla
                    listMapped.ListItems.Clear();
                    foreach (var item in Fechas)
                    {
                        listMapped.ListItems.Add(_mapper.Map<GetFechaDTO>(item));
                    }
                    return Ok(listMapped);
                }
                return BadRequest(new { code = 404, message = "No hay elementos para mostrar" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay elementos para mostrar: {e.Message}" });
            }
        }

        //Se trae una fecha
        [HttpGet("{id:guid}/Get")]
        public async Task<IActionResult> GetUniqueFecha([FromRoute] Guid id)
        {
            try
            {
                //Get de un item con LINQ + DTO
                var toDoItems = from item in dbContext.Fechas
                                where item.State && item.Id == id
                                select new GetFechaDTO()
                                {
                                    Fecha = item.Fecha,
                                    State = item.State
                                };

                if (toDoItems.Any() && toDoItems != null)
                {
                    return Ok(toDoItems);
                }
                return BadRequest(new { code = 404, message = "No hay un elemento con este id" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay un elemento con este id: {e.Message}" });
            }
        }
        //Añadir fechas con DTO
        [HttpPost]
        public async Task<IActionResult> AddItem(AddFechaDTO addFechaDTO)
        {
            try
            {
                var Fechas = _mapper.Map<FechaModel>(addFechaDTO);
                {
                    Fechas.State = true;
                };

                await dbContext.Fechas.AddAsync(Fechas);
                await dbContext.SaveChangesAsync();

                return Ok(Fechas);
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo añadir el elemento: {e.Message}" });
            }
        }
        //Actulizar item completo DTO
        [HttpPut("{id:guid}/UpdateAll")]
        public async Task<IActionResult> UpdateAllItem([FromRoute] Guid id, UpdateFechaDTO updateToDoItemDTO)
        {
            try
            {
                var ToDoItem = await dbContext.Fechas.Where(list => list.State && list.Id == id)
                    .ToListAsync();

                if (ToDoItem.Count != 0 && ToDoItem != null)
                {
                    foreach (var item in ToDoItem)
                    {
                        item.Fecha = updateToDoItemDTO.Fecha;
                    }
                    await dbContext.SaveChangesAsync();
                    return Ok(ToDoItem);
                }
                return BadRequest(new { code = 404, message = "No hay un elemento con este id" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo modificar el elemento: {e.Message}" });
            }
        }

        //delete item con DTO
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            try
            {
                var ToDoItem = await dbContext.Fechas.Where(list => list.State && list.Id == id)
                    .ToListAsync();

                if (ToDoItem.Count != 0 && ToDoItem != null)
                {
                    foreach (var item in ToDoItem)
                    {
                        item.State = false;
                    }
                    await dbContext.SaveChangesAsync();
                    return Ok(ToDoItem);
                }
                return BadRequest(new { code = 404, message = "No hay un elemento con este id" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo eliminar el elemento: {e.Message}" });
            }
        }
    }
}
