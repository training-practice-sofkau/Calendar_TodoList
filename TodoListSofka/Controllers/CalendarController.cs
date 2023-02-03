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
			var tareaActiva = dbContext.Eventos_Calendario.Where(r =>r.Id != 0 && r.Tareas.Count != 0).Include(r => r.Tareas).ToList();
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

		[HttpGet("{dia}")]
		public async Task<IActionResult> Get(int dia)
		{
			try
			{
				var tarea = dbContext.Eventos_Calendario.Where(r => r.Dia == dia).Include(r => r.Tareas).ToList();
				if (tarea == null || dia == 0 )
				{
					return BadRequest(new { code = 400, message = "Dia no encontrado. " });
				}
				else
				{
					return Ok(tarea);
				}
			}
			catch (Exception ex)
			{
				return NotFound(new { code = 404, message = $"Dia no encontrado. : {ex.Message}" });
			}
		}

		
		[HttpPost]
		public async Task<Object> Post(CrearCalendarioDto eventoDto)
		{
			var url = "https://localhost:7281/api/ToDo";
			var nuevoDia = new CalendarModel();
			var nuevaTarea = new TareaModel();
			int idTarea = 0;

			nuevoDia.Dia = eventoDto.Dia;
			nuevoDia.Mes = 2;
			nuevoDia.Anio = 2023;

			var tarea = dbContext.Eventos_Calendario.Where(r => r.Dia == eventoDto.Dia).Include(r => r.Tareas).ToList();
			
			if (tarea.Count == 0)
			{
				dbContext.Add(nuevoDia);
				await dbContext.SaveChangesAsync();
				tarea = dbContext.Eventos_Calendario.Where(r => r.Dia == eventoDto.Dia).Include(r => r.Tareas).ToList();
			}

			foreach (var item in tarea)
			{
				idTarea = item.Id;
			}

			nuevaTarea.Title = eventoDto.Title;
			nuevaTarea.Description = eventoDto.Description;
			nuevaTarea.Responsible = eventoDto.Responsible;
			nuevaTarea.Priority = eventoDto.Priority;
			nuevaTarea.IsCompleted = eventoDto.IsCompleted;
			nuevaTarea.CalendarModelId = idTarea;

			string jsonString = JsonSerializer.Serialize(nuevaTarea);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

			try
			{
				var response = await client.PostAsync(url, content);
				return Ok(jsonString);
			}catch(Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpPost("/Agregar/Usuarios")]
		public async Task<Object> PostUsuarios(CrearCalendarioDto eventoDto)
		{
			Tareas.Instance.crearUsuarios();
			return Ok("Usuarios creados");
		}

		[HttpPut("/Dia/{dia}/Tarea/{idTarea}")]
		public async Task<Object> Put(int dia, int idTarea, TareaDto data)
		{
			var url = "https://localhost:7281/api/ToDo";
			var updateTarea = new TareaModel();
			int idDia;
			bool ban = false;

			if (data == null || dia == 0 || idTarea == 0)
				return BadRequest("Datos ingresados errados. ");

			var tarea = dbContext.Eventos_Calendario.Where(r => r.Dia == dia && r.Tareas.Count != 0).Include(r => r.Tareas).ToList();

			if (tarea.Count == 0)
				return BadRequest("No hay eventos programados este dia. ");

			foreach (var item in tarea)
			{
				foreach (var item1 in item.Tareas)
				{
					if (item1.Id == idTarea)
					{
						ban= true;
					}
				}
			}

			if(ban)
			{
				updateTarea.Id = idTarea;
				updateTarea.Title = data.Title;
				updateTarea.Description = data.Description;
				updateTarea.Responsible = data.Responsible;
				updateTarea.Priority = data.Priority;
				updateTarea.IsCompleted = data.IsCompleted;
			}
			else
			{
				return BadRequest("Id de tarea no encontrado.");
			}

			string jsonString = JsonSerializer.Serialize(updateTarea);
			var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

			try
			{
				var response = await client.PutAsync(url, content);
				Calendario.Instance.Dia = dia;
				Tareas.Instance.notificarEvento();
				return Ok(jsonString);
			}
			catch (DbUpdateConcurrencyException ex)
			{
				return NotFound(new { code = 404, message = $"Error en datos ingresados. : {ex.Message}" });
			}

		}

		[HttpDelete("/Dia/{dia}/Tarea/{idTarea}")]
		public async Task<Object> Delete(int dia, int idTarea)
		{
			var url = "https://localhost:7281/api/ToDo/"+idTarea;
			int idDia;
			bool ban = false;

			if ( dia == 0 || idTarea == 0)
				return BadRequest("Datos ingresados errados. ");

			var tarea = dbContext.Eventos_Calendario.Where(r => r.Dia == dia && r.Tareas.Count != 0).Include(r => r.Tareas).ToList();

			if (tarea.Count == 0)
				return BadRequest("No hay eventos programados este dia. ");

			foreach (var item in tarea)
			{
				foreach (var item1 in item.Tareas)
				{
					if (item1.Id == idTarea)
					{
						ban = true;
					}
				}
			}

			try
			{
				if (ban)
				{
					var response = await client.DeleteAsync(url);
					return Ok();
				}
				else
				{
					return BadRequest("Id de tarea no encontrado.");
				}
				
			}
			catch (DbUpdateConcurrencyException ex)
			{
				return BadRequest();
			}

		}

	}
}