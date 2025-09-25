using Bank.Server.Data;
using Bank.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace Bank.Server.Services
{
    public class AuthService
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return result;

            if (await roleManager.RoleExistsAsync("User"))
            {
                await userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }

        public async Task<LoginResp?> LoginAsync(LoginRequest model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
                return null;

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return null;

            if (user is ApplicationUser applicationUser)
            {
                applicationUser.LastLogin = DateTime.UtcNow;
            }

           var updateResult = await userManager.UpdateAsync(user);
            if(!updateResult.Succeeded)
            {
                return null;
            }

            var currentRoles = await userManager.GetRolesAsync(user);

            return new LoginResp
            {
                Roles = currentRoles
            };
        }
    }
}
