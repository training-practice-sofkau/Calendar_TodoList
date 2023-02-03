
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Dto
{
    public class ItemActualizar
    {
        [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
        public string Descripccion { get; set; } = null!;
        
        [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
        public string? Resposible { get; set; }
        
        [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
        [Range(1,28)]
        public int? IdCalendar { get; set; }

    }
}
