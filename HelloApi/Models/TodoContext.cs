using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Models
{
	public class TodoContext : DbContext
	{
		public TodoContext(DbContextOptions<TodoContext> options) : base(options)
		{
		}

		public DbSet<TodoItem> TodoItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .HasData(
                    new TodoItem
                    {
                        Id = 1,
                        Name = "Make a cup of tea",
                        IsComplete = true
                    },
                    new TodoItem
                    {
                        Id = 2,
                        Name = "Tidy your code",
                        IsComplete = false
                    },
                    new TodoItem
                    {
                        Id = 3,
                        Name = "Learn something new",
                        IsComplete = false
                    }
                );
        }
    }
}

