using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Models
{
	public class TareaModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; } = null!;

		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; } = null!;

		[Required(ErrorMessage = "Responsible is required")]
		public string Responsible { get; set; } = null!;

		[Required(ErrorMessage = "Priority is required")]
		public string Priority { get; set; } = null!;

		[Required(ErrorMessage = "IsCompleted is required")]
		public bool IsCompleted { get; set; }
		public bool State { get; set; }
		[Required(ErrorMessage = "IsCompleted is required")] public int CalendarModelId { get; set; }

		public TareaModel(int id, string title, string description, string responsible,
			bool isCompleted, bool state)
		{
			Id = id;
			Title = title;
			Description = description;
			Responsible = responsible;
			IsCompleted = isCompleted;
			State = state;
		}

		public TareaModel() { }



	}
}