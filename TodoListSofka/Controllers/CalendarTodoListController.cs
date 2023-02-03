using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using TodoListSofka.Dto;
using TodoListSofka.Models;

namespace TodoListSofka.Controllers
{
    [Route("api/Controlador")]
    [ApiController]
    public class CalendarTodoListController : ControllerBase
    {


        private readonly CalendarTodoListContext _dbContext;
        public IJornada jorn;

        public CalendarTodoListController(CalendarTodoListContext dbContext){

            _dbContext = dbContext;

        }

        /// <summary>
        /// Listo funcional con validaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllDaysAndItems")]
        public async Task<IActionResult> GetCalendars()
        {
            try
            {

                var activeRecords = await _dbContext.Calendars.Include(r => r.Items.Where(x => x.Estate != 0)).ToListAsync();
                if (activeRecords == null)
                {

                    return BadRequest(new { 
                        
                        code = 404,
                        message = "No hay tareas para mostrar"
                    
                    });

                }

                return Ok((activeRecords));
            }

            catch (Exception e)
            {

                return BadRequest(new
                {

                        code = 404,
                        message = $"No hay tareas para mostrar:  {e.Message}"

            });
                
            }

        }
       
        /// <summary>
        /// Listo Funcional con validaciones
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/OneDay/{id:int}")]
        public async Task<IActionResult> GetDayAndItem([FromRoute] int id)
        {
            try
            {
                var day = await _dbContext.Calendars.FindAsync(id);
                var task = from aux in _dbContext.Items where (aux.IdCalendar == id && aux.Estate != 0) select aux;
                if (day == null)
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "No hay tareas para mostrar"

                    });

                }

                if (task == null)
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "No hay tareas para mostrar"

                    });

                }

                return Ok(task);

            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = $"No hay tareas para mostrar:  {e.Message}"
                });

            }
            finally { 
            
            
            
            
            }

        }
        
        /// <summary>
        /// Listo funcional con validaciones
        /// </summary>
        /// <param name="id"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/CompletedOneTask/{id:int}")]
        public async Task<IActionResult> CompleteOneItem([FromRoute] int id, bool complete)
        {

            try
            {
                var respon = await _dbContext.Items.FindAsync(id);

                if (respon.Estate == 0){

                    return BadRequest(new
                    {

                        code = 400,
                        message = "Posiblemente  se haya eliminado esa tarea"

                    }) ;

                }

                if (respon == null)
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "No existe esa tarea para poderla finalizar ingrese una tarea que exista"

                    });

                }
                if (complete != true || complete != false)
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "Ingresar el campo booleano por favor"

                    });

                }

                respon.IsCompleted = complete;
                await _dbContext.SaveChangesAsync();
                return Ok($"Tarea finalizada con exíto");

            }

            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(new
                {

                    code = 400,
                    message = $"Ocurrio un error en la actualizacion {e.Message}"

                });


            }
            finally { 
            
            
            
            }

        
        }

        /// <summary>
        /// Listo funcional con validaciones
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoitemAc"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/UpdateAllTask/{id:int}")]
        public async Task<IActionResult> updateTaks([FromRoute] int id, ItemActualizar todoitemAc)
        {
            try
            {
                var item = await _dbContext.Items.FindAsync(id);

                if (item == null)
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "No existe esa tarea para poderla actualizar"

                    });
                }

                if (item.Estate == 0)
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "Posiblemente  se haya eliminado esa tarea"

                    });

                }

                item.Title = todoitemAc.Title;
                item.Descripccion = todoitemAc.Descripccion;
                item.Resposible = todoitemAc.Resposible;
                item.IsCompleted = todoitemAc.IsCompleted;
                item.IdCalendar = todoitemAc.IdCalendar;
                await _dbContext.SaveChangesAsync();
                return Ok($"Tarea actualizada, {item}");

            }

            catch (DbUpdateConcurrencyException e)
            {

                return BadRequest(new
                {

                    code = 400,
                    message = $"Ocurrio un error en la actualizacion {e.Message}"

                });


            }
            catch (Exception f) {

                return BadRequest(new
                {

                    code = 400,
                    message = $"Ocurrio un error en la actualizacion {f.Message}"

                });




            }
            finally
            {



            }


        }

        /// <summary>
        /// Listo  funcional sin las validaciones
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dayUser"></param>
        /// <param name="jornadaUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostItem(ItemAgregar item, int dayUser, string jornadaUser)
        {

            try
            {
                jorn = Day.CreateJornada(jornadaUser);
                var day = await _dbContext.Calendars.FindAsync(dayUser);
                if (dayUser > 28 || dayUser < 1)
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "Por favor ingresa un dia valido del mes de febrero"

                    });


                }
                if (day == null)
                {

                    
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
                    var listjornadasalmacenadasMorning = jorn.ShowJornada(itemcreado);

                    return Ok("Tarea Creada Con Exito " + listjornadasalmacenadasMorning.ToString());
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
                var listjornadasalmacenas = jorn.ShowJornada(itemcreado2);
                return Ok("Tarea Creada Con Exito " + listjornadasalmacenas.ToString());
           
            }
            catch (Exception e)
            {
                return BadRequest(new
                {

                    code = 400,
                    message = $"Ocurrio un error {e.Message}"

                });
            }






        }


        [HttpDelete]
        [Route("/DeleteTask/{id:int}")]
        public async Task<ActionResult> DeleteItem([FromRoute] int id)
        {

            var recordToUpdate = _dbContext.Items.FirstOrDefault(r => r.Id == id);

            try
            {

                if (recordToUpdate == null)
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "No existe esa tarea para poderla eliminar"

                    });
                }

                if (recordToUpdate.Estate == 0)
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "Esa tarea ya fue eliminada"

                    });
                }

                if (recordToUpdate != null)
                {
                    recordToUpdate.Estate = 0;
                    _dbContext.SaveChanges();
                }

                return Ok(new
                {
                    code = 200,
                    message = $"La tarea con id {id} fue eliminada"
                }
                );
            }
            catch (Exception e)
            {

                return BadRequest(new
                {

                    code = 400,
                    message = $"Ocurrio un error {e.Message}"

                });




            }
            finally { 
            
            
            
            
            
            }


        }


    }


}
