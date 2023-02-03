using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class CalendarioDTO
    {
        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public int Dia { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public int Jornada { get; set; }
    }
}
