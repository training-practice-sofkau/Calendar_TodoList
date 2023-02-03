using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoListSofka.Models;
using TodoListSofka1.Models;

namespace TodoListSofka1.Data
{
	public class CalendarApiDbContext : DbContext
	{
		public DbSet<CalendarModel> Eventos_Calendario { get; set; }
		public DbSet<TareaModel> Tarea { get; set; }

		public CalendarApiDbContext(DbContextOptions options) : base(options)
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
