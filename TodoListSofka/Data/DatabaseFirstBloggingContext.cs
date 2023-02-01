using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Model;

namespace TodoListSofka.Data;

public partial class DatabaseFirstBloggingContext : DbContext
{
    public DatabaseFirstBloggingContext()
    {
    }
    public DatabaseFirstBloggingContext(DbContextOptions<DatabaseFirstBloggingContext> options)
        : base(options)
    {
    }
    public DbSet<ToDoItem> ToDoItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__ToDoItem__727E838BE669966F");

            entity.ToTable("ToDoItem");

            entity.Property(e => e.ItemId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Responsible).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
