using AutoMapper;
using TodoListSofka.DTO;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //Modelos de Fecha
            CreateMap<FechaModel, GetFechaDTO>().ReverseMap();
            CreateMap<FechaModel, AddFechaDTO>().ReverseMap();
            CreateMap<FechaModel, UpdateFechaDTO>().ReverseMap();

            //Modelos de Tarea

        }
    }
}
