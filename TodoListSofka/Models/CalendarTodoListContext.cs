using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoListSofka.Models;

public partial class CalendarTodoListContext : DbContext
{
    public CalendarTodoListContext()
    {
    }

    public CalendarTodoListContext(DbContextOptions<CalendarTodoListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Item> Items { get; set; }

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.NumberDaY).HasName("PK__Calendar__E0C22DE8989BB7DA");

            entity.ToTable("Calendar");

            entity.Property(e => e.NumberDaY).ValueGeneratedNever();
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Item__3214EC07976D1CB7");

            entity.ToTable("Item");

            entity.Property(e => e.Descripccion).HasMaxLength(70);
            entity.Property(e => e.Resposible).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(20);

            entity.HasOne(d => d.IdCalendarNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdCalendar)
                .HasConstraintName("FK__Item__IdCalendar__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
