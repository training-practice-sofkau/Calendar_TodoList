using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoListSofka.Models;

public partial class Todoitem
{
    public Guid Id { get; set; }

    public Guid IdCalendar { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string Responsible { get; set; } = null!;


    [System.ComponentModel.DefaultValue(false)]
    public bool IsCompleted { get; set; } = false;

    [System.ComponentModel.DefaultValue(true)]
    public bool State { get; set; } = true;

    public virtual Calendar? IdCalendarNavigation { get; set; }
}

/*
 Scaffold-DbContext "Server=LAPTOP-UVJ9P68E\MSSQLSERVER01;Database=calendardb;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
 */