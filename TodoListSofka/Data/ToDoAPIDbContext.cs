using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoListSofka.Model;

namespace TodoListSofka.Data
{
    public class ToDoAPIDbContext : DbContext
    {
        public DbSet<TodoItem> ListaTareas1 { get; set; }

        public DbSet<Day> Dias { get; set; }

        public ToDoAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted &&
                e.Metadata.GetProperties().Any(x => x.Name == "State")))
            {
                item.State = EntityState.Unchanged;
                item.CurrentValues["State"] = false;
            }

            return base.SaveChanges();
        }
    
    }
}
