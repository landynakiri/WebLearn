using Microsoft.EntityFrameworkCore;

namespace WL.Server.Model
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems => Set<TodoItem>();

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
    }
}
