using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class ToDoUpdateDto
    {
        [Required] public int Id { get; set; }
        [Required] public string Title { get; set; } = null!;
        [Required] public string Description { get; set; } = null!;
        [Required] public string Responsible { get; set; } = null!;
        [Required] public string Priority { get; set; } = null!;
        public bool IsCompleted { get; set; }

        public ToDoUpdateDto(int id, string title, string description, string responsible, string priority,
            bool isCompleted)
        {
            Id = id;
            Title = title;
            Description = description;
            Responsible = responsible;
            Priority = priority;
            IsCompleted = isCompleted;
        }

        public ToDoUpdateDto() { }
    }
}
