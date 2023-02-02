
using Microsoft.Extensions.Options;
using System.Text.Json;
using TodoListSofka.Models;
using TodoListSofka1.Data;

namespace TodoListSofka1.Logica
{
	public class Calendario
	{
		static HttpClient client = new HttpClient();
		JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

		public async Task<Object> obtenerTareas()
		{
			var url = "https://localhost:7281/api/ToDo";
			var response = await client.GetAsync(url);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var contenidoDesseria = JsonSerializer.Deserialize<List<TareaModel>>(content, options);
				return contenidoDesseria;
			}
			return null;
		}

		
	}
}
