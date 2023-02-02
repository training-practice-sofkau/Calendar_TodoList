using System.ComponentModel.DataAnnotations;
using TodoListSofka.Models;

namespace TodoListSofka1.DTO
{
	public class CrearCalendarioDto
	{
		[Required] public int Dia { get; set; }
		[Required] public string Title { get; set; } = null!;
		[Required] public string Description { get; set; } = null!;
		[Required] public string Responsible { get; set; } = null!;
		[Required] public string Priority { get; set; } = null!;
		public bool IsCompleted { get; set; }

		[Required(ErrorMessage = "CalendarId is required")] public int CalendarModelId { get; set; }
		public CrearCalendarioDto(int dia)
		{
			Dia = dia;
		}

		public CrearCalendarioDto() { }

	}
}