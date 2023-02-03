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

        //Agrego Tareas y Fechas
        [HttpPost]
        public async Task<IActionResult> AddFechaTarea(AddFechaTareaDTO AddFechaTareaDTO)
        {
            try
            {

                var Tarea = _mapper.Map<TareaModel>(AddFechaTareaDTO.AddTareaDTO);
                {
                    Tarea.State = true;
                }

                await dbContext.Tareas.AddAsync(Tarea);
                await dbContext.SaveChangesAsync();

                var Fechas = _mapper.Map<FechaModel>(AddFechaTareaDTO.AddFechaDTO);
                {
                    var Fecha = Fechas.Fecha;
                    Fechas.Dia = Fecha.Day;
                    Fechas.Mes = Fecha.Month;
                    Fechas.Año = Fecha.Year;
                    Fechas.State = true;
                    Fechas.IdEventos = Tarea.Id;
                };
                if (Fechas.Dia <= 29 && Fechas.Dia > 0 && Fechas.Mes == 2)
                {
                    await dbContext.Fechas.AddAsync(Fechas);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    return BadRequest(new { code = 400, message = $"No se pudo añadir el elemento Digite bien la fecha" });
                }
                return Ok("Datos Guardados: " + " " + Fechas + " " + Tarea);
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo añadir el elemento: {e.Message}" });
            }
        }

        //Saco todos las fechas
        [HttpGet("GetAllFecha/Get")]
        public async Task<IActionResult> GetAllFecha()
        {
            try
            {
                var Fechas = await dbContext.Fechas.Where(list => list.State).ToListAsync();

                //Creamos la instancia del creador de listas (Singleton Pattern)
                CreatorList listMapped = CreatorList.GetInstance();

                if (Fechas.Count != 0 && Fechas != null)
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

         [HttpGet("GetAllTarea/Get")]
         public async Task<IActionResult> GetAllTarea()
         {
             try
             {
                 var Tareas = await dbContext.Tareas.Where(list => list.State).ToListAsync();

                 //Creamos la instancia del creador de listas (Singleton Pattern)
                 CreatorList listMapped2 = CreatorList.GetInstance();

                 if (Tareas.Count != 0 && Tareas != null)
                 {
                     listMapped2.ListItems2.Clear();

                     foreach (var item in Tareas)
                     {
                         listMapped2.ListItems2.Add(_mapper.Map<GetTareaDTO>(item));
                     }
                     return Ok(listMapped2);
                 }
                 return BadRequest(new { code = 404, message = "No hay elementos para mostrar" });
             }
             catch (Exception e)
             {
                 return BadRequest(new { code = 404, message = $"No hay elementos para mostrar: {e.Message}" });
             }
         }


        [HttpGet("GetTareaDiurno/Get")]
        public async Task<IActionResult> GetTareaDiurno()
        {
            try
            {
                var Tareas = await dbContext.Tareas.Where(list => list.Jornada == "Diurno").ToListAsync();

                //Creamos la instancia del creador de listas (Singleton Pattern)
                CreatorList listMapped2 = CreatorList.GetInstance();

                if (Tareas.Count != 0 && Tareas != null)
                {
                    listMapped2.ListItems2.Clear();

                    foreach (var item in Tareas)
                    {
                        listMapped2.ListItems2.Add(_mapper.Map<GetTareaDTO>(item));
                    }
                    return Ok(listMapped2);
                }
                return BadRequest(new { code = 404, message = "No hay elementos para mostrar" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay elementos para mostrar: {e.Message}" });
            }
        }

        [HttpGet("GetTareaNocturno/Get")]
        public async Task<IActionResult> GetTareaNocturno()
        {
            try
            {
                var Tareas = await dbContext.Tareas.Where(list => list.Jornada == "Nocturno").ToListAsync();

                //Creamos la instancia del creador de listas (Singleton Pattern)
                CreatorList listMapped2 = CreatorList.GetInstance();

                if (Tareas.Count != 0 && Tareas != null)
                {
                    listMapped2.ListItems2.Clear();

                    foreach (var item in Tareas)
                    {
                        listMapped2.ListItems2.Add(_mapper.Map<GetTareaDTO>(item));
                    }
                    return Ok(listMapped2);
                }
                return BadRequest(new { code = 404, message = "No hay elementos para mostrar" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay elementos para mostrar: {e.Message}" });
            }
        }

        /*
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
        }*/

        /*  //Actulizar item completo DTO
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
          }*/
    }
}
