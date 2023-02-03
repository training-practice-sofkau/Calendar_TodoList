﻿using Microsoft.AspNetCore.Mvc;
using TodoListSofka.Data;
using TodoListSofka.DTO;
using TodoListSofka.DTO.Day;
using TodoListSofka.Model;

namespace TodoListSofka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayController : ControllerBase
    {

        private readonly ToDoAPIDbContext dbContext;

        public DayController(ToDoAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }       


        [HttpPost]
        public async Task<Object> Post(TodoCreateDayDTO dayDTO)
        {
            var nuevafecha = new Day();
            nuevafecha.dayNumber = dayDTO.dayNumber;
            nuevafecha.dayWeek = dayDTO.dayWeek;

            dbContext.Add(nuevafecha);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
