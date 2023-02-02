using System.ComponentModel.DataAnnotations;
using TodoListSofka.Models;

namespace TodoListSofka1.Models
{
	public class CalendarModel
	{
		[Required]public int Id { get; set; }
		[Required]public int Dia { get; set; }

		public int Mes { get; set; } = 02;

		public int Anio { get; set; } = 2023;

		public bool State { get; set; }
		public List<TareaModel> Tareas { get; set; }


		public CalendarModel(int dia)
		{
			Dia = dia;
		}

		public CalendarModel() { }

	}
}
