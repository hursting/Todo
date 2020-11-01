using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Todo.Api
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
           : base(options)
        {
            
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public class ToDoContextFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<ToDoContext>
        {
            public ToDoContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ToDoContext>();                
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("SqlConnectionString"));
                return new ToDoContext(optionsBuilder.Options);
            }
        }
    }
}
