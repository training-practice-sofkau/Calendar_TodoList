using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.DTO
{
    public class UpdateTareaDTO
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Jornada { get; set; } = null!;
    }
}
