using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Bank.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
        { }
    }
}
