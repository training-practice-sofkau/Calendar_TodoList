using Microsoft.AspNetCore.Mvc;
using TodoListSofka.Models;

namespace TodoListSofka.Controllers
{
    public interface IJornada
    {

        public Object ShowJornada(Item tarea);


    }
}
