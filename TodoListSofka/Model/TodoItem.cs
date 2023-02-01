using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Model;

public class ToDoItem
{
    public Guid ItemId { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string Responsible { get; set; } = null!;
    [Required]
    public bool IsCompleted { get; set; }
    [Required]
    public bool State { get; set; }
    public ToDoItem(Guid id, string title, string description, string responsible,
            bool isCompleted, bool state)
    {
        ItemId = id;
        Title = title;
        Description = description;
        Responsible = responsible;
        IsCompleted = isCompleted;
        State = state;
    }
    public ToDoItem() { }
}
