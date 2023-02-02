using System.ComponentModel.DataAnnotations;

namespace TodoListSofka1.DTO
{
	public class TareaDto
	{

		[Required] public string Title { get; set; } = null!;
		[Required] public string Description { get; set; } = null!;
		[Required] public string Responsible { get; set; } = null!;
		[Required] public string Priority { get; set; } = null!;
		public bool IsCompleted { get; set; }

		[Required(ErrorMessage = "CalendarId is required")] public int CalendarModelId { get; set; }
		public TareaDto(string title, string description, string responsible, string priority,
			bool isCompleted)
		{
			Title = title;
			Description = description;
			Responsible = responsible;
			Priority = priority;
			IsCompleted = isCompleted;
		}

		public TareaDto() { }
	}
}
