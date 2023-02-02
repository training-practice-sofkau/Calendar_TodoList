﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.DTO;
using TodoListSofka.Models;

namespace TodoListSofka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoitemController : ControllerBase
    {
        private readonly CalendardbContext _CalendardbContext;

        public TodoitemController(CalendardbContext calendardbContext)
        {
            _CalendardbContext = calendardbContext;
        }

        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> CreateTodoItem(Guid id, TodoitemDTO todoItemDTO)
        {

            var todoItem = new Todoitem
            {
                IdCalendar = id,
                Title = todoItemDTO.Title,
                Description = todoItemDTO.Description,
                Responsible = todoItemDTO.Responsible
            };

            await _CalendardbContext.Todoitems.AddAsync(todoItem);
            await _CalendardbContext.SaveChangesAsync();

            return Ok(todoItem);
        }

    }
}
