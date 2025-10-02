using Bank.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Server.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }
        public DbSet<Test> Tests { get; set; }
    }
}
