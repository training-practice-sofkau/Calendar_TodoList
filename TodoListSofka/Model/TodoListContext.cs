using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoListSofka.Model;

public partial class TodoListContext : DbContext
{
    public TodoListContext()
    {
    }

    public TodoListContext(DbContextOptions<TodoListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendario> Calendarios { get; set; }

    public virtual DbSet<Todoitem> Todoitems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CALENDAR__3214EC07E09074CA");

            entity.ToTable("CALENDARIO");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Todoitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Todoitem__3214EC075D1AF700");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Responsible)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCalendarNavigation).WithMany(p => p.Todoitems)
                .HasForeignKey(d => d.IdCalendar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Todoitems__IdCal__6477ECF3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
