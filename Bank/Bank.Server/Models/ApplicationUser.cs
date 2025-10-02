using Microsoft.AspNetCore.Identity;

namespace Bank.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; } = DateTime.UtcNow;
    }
}
