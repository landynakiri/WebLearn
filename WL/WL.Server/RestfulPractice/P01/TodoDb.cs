using Microsoft.EntityFrameworkCore;

namespace WL.Server.RestfulPractice.P01
{
    class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options)
        {
        }
        public DbSet<Todo> Todos => Set<Todo>();
    }
}
