using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoListSofka.DTO
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
    }
}
