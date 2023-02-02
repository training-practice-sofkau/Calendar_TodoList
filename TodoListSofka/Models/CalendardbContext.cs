using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoListSofka.Models;

public partial class CalendardbContext : DbContext
{
    public CalendardbContext()
    {
    }

    public CalendardbContext(DbContextOptions<CalendardbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Todoitem> Todoitems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-UVJ9P68E\\MSSQLSERVER01;Database=calendardb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calendar__3214EC0778B20EDD");

            entity.ToTable("Calendar");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Day).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Todoitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Todoitem__3214EC0714C19C54");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Responsible)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCalendarNavigation).WithMany(p => p.Todoitems)
                .HasForeignKey(d => d.IdCalendar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Todoitems__IdCal__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
