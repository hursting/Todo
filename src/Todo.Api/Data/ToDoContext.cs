using Microsoft.EntityFrameworkCore;

namespace Todo.Api
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
           : base(options)
        { }


        public DbSet<Post> Posts { get; set; }

    }
}
