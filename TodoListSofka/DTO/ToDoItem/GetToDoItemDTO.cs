using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO.ToDoItem
{
    public class GetToDoItemDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Responsible { get; set; } = null!;
        [Required]
        public bool IsCompleted { get; set; }

        public GetToDoItemDTO()
        {
        }
        public GetToDoItemDTO(string title, string description, string responsible)
        {
            Title = title;
            Description = description;
            Responsible = responsible;
        }
        private static GetToDoItemDTO _instance;
        public static GetToDoItemDTO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GetToDoItemDTO();
            }
            return _instance;
        }
    }
}
