using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TodoListSofka.Dto
{
    public class ItemAgregar
    {
        [Required(ErrorMessage ="Por favor ingresar el dato, no dejar el dato vacio")]
        public string Title { get; set; } = null!;                      
        
        [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
        public string Descripccion { get; set; } = null!;            
        
        [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
       public string? Resposible { get; set; }                     
        
        [Required(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
        public bool IsCompleted { get; set; }
        /*
        ired(ErrorMessage = "Por favor ingresar el dato, no dejar el dato vacio")]
        public int IdCalendar { get; set; }
        */
    }
}
