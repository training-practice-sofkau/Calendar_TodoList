using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;
using System.Threading;
using TodoListSofka.Models;
using TodoListSofka1.Data;
using TodoListSofka1.DTO;
using TodoListSofka1.Logica;
using TodoListSofka1.Models;
using System.Text.Json.Serialization;
using System.Text;
using Azure;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
		private readonly CalendarApiDbContext dbContext;
		static HttpClient client = new HttpClient();
		JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

		public CalendarController(CalendarApiDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetDias()
		{
			//Busca las Tareas que no hayan sido eliminados y los retorna
			var tareaActiva = dbContext.Eventos_Calendario.Where(r =>r.State != false).Include(r => r.Tareas).ToList();
			return Ok(tareaActiva);

		}

		[HttpGet("api/tareas")]
		public async Task<IActionResult> GetTareas()
		{
			var url = "https://localhost:7281/api/ToDo";
			var response = await client.GetAsync(url);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var contenidoDesseria = JsonSerializer.Deserialize<List<TareaModel>>(content, options);
				return Ok(contenidoDesseria);
			}
			return BadRequest();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var tarea = dbContext.Eventos_Calendario.Where(r => r.State && r.Dia == id).Include(r => r.Tareas).ToList();
				//var tarea = dbContext.Eventos_Calendario.Where(r => r.State && r.Id == id).Include(r => r.Tareas).ToList();
				if (tarea == null || id == 0 )
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

		
		[HttpPost]
		public async Task<Object> Post(CrearCalendarioDto eventoDto)
		{
			var url = "https://localhost:7281/api/ToDo";
			var nuevoDia = new CalendarModel();
			var nuevaTarea = new TareaDto();

			nuevoDia.Dia = eventoDto.Dia;
			nuevoDia.Mes = 2;
			nuevoDia.Anio = 2023;
			nuevoDia.State = true;

			dbContext.Add(nuevoDia);

			nuevaTarea.Title = eventoDto.Title;
			nuevaTarea.Description = eventoDto.Description;
			nuevaTarea.Responsible = eventoDto.Responsible;
			nuevaTarea.Priority = eventoDto.Priority;
			nuevaTarea.IsCompleted = eventoDto.IsCompleted;
			nuevaTarea.CalendarModelId = eventoDto.CalendarModelId;


			string jsonString = JsonSerializer.Serialize(nuevaTarea);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

			try
			{
				await dbContext.SaveChangesAsync();
				var response = await client.PostAsync(url, content);
				return Ok(response);
			}catch(Exception ex)
			{
				return ex.Message;
			}
			
			

			
		}

	}
}