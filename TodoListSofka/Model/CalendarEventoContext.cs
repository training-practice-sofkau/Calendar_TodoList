using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoListSofka.Model;

public partial class CalendarEventoContext : DbContext
{
    public CalendarEventoContext()
    {
    }

    public CalendarEventoContext(DbContextOptions<CalendarEventoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fecha> Fechas { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-LCKAILD;Database=CalendarEvento;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fecha>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fechas__3214EC074E38961F");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Fecha1)
                .HasColumnType("datetime")
                .HasColumnName("Fecha");
            entity.Property(e => e.IdEventos).HasColumnName("Id_Eventos");

            entity.HasOne(d => d.IdEventosNavigation).WithMany(p => p.Fechas)
                .HasForeignKey(d => d.IdEventos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fechas__Id_Event__398D8EEE");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tareas__3214EC07C4F337D7");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
