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

		public List<TareaModel> Tareas { get; set; }


		public CalendarModel(int dia, int mes, int anio)
		{
			Dia = dia;
			Mes = mes;
			Anio = anio;
		}

		public CalendarModel() { }

	}
}
