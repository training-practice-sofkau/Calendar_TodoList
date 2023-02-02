using System;
using System.Collections.Generic;

namespace TodoListSofka.Models;

public partial class Todoitem
{
    public Guid Id { get; set; }

    public Guid IdCalendar { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Responsible { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public bool State { get; set; }

    public virtual Calendar? IdCalendarNavigation { get; set; }
}

/*
 Scaffold-DbContext "Server=LAPTOP-UVJ9P68E\MSSQLSERVER01;Database=calendardb;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
 */