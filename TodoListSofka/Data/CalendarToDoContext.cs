using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Model;

namespace TodoListSofka.Data;

public partial class CalendarToDoContext : DbContext
{
    public CalendarToDoContext()
    {
    }
    public CalendarToDoContext(DbContextOptions<CalendarToDoContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Calendar> Calendars { get; set; }
    public virtual DbSet<ToDoItem> ToDoItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.IndexDay).HasName("PK__Calendar__572830A2827AADA5");

            entity.ToTable("Calendar");

            entity.Property(e => e.IndexDay).ValueGeneratedNever();
        });

        modelBuilder.Entity<ToDoItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__ToDoItem__727E838B230F7070");

            entity.ToTable("ToDoItem");

            entity.Property(e => e.ItemId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Responsible).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.IndexDayNavigation).WithMany(p => p.ToDoItems)
                .HasForeignKey(d => d.IndexDay)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.ToDoItem_dbo.Calendar_IndexDay");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
