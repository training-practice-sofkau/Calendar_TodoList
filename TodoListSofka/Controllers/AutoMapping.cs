using AutoMapper;
using System.Net;
using TodoListSofka.DTO;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ToDoItem, GetToDoItemDTO>().ReverseMap();
            CreateMap<ToDoItem, AddToDoItemDTO>().ReverseMap();
            CreateMap<ToDoItem, UpdateToDoItemDTO>().ReverseMap();
        }
    }
}
