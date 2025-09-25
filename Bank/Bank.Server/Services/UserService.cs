using Bank.Server.Data;
using Bank.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bank.Server.Services
{
    public class UserService
    {
        private UserManager<ApplicationUser> userManager;

        public UserService(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> SetRolesAsync(string userId, string[] roles)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);
            await userManager.AddToRolesAsync(user, roles);
            return true;
        }

        public async Task<IEnumerable<GetUserResp>> GetUsers()
        {
            var users = await userManager.Users.ToListAsync();

            var result = new List<GetUserResp>();
            foreach (var user in users)
            {
                var applicationUser = user as ApplicationUser;
                var roles = await userManager.GetRolesAsync(user);
                result.Add(new GetUserResp
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles,
                    CreatedAt = applicationUser?.CreatedAt,
                    LastLogin = applicationUser?.LastLogin
                });
            }

            return result;
        }
    }
}
