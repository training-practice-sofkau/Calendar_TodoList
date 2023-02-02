using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;
using TodoListSofka.Models;
using TodoListSofka1.Data;
using TodoListSofka1.Logica;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
		private readonly CalendarApiDbContext dbContext;

		public CalendarController(CalendarApiDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetPersonajes()
		{
			//Busca las Tareas que no hayan sido eliminados y los retorna
			var tareaActiva = dbContext.Eventos_Calendario.Where(r =>r.Id == 1).Include(r => r.Tareas).ToList();
			return Ok(tareaActiva);

			//Muestra todos los personajes 
			//return await dbContext.Tareas.ToListAsync();
		}
	}
}