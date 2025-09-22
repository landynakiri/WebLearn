using Bank.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (await roleManager.RoleExistsAsync("User"))
            {
                await userManager.AddToRoleAsync(user, "User");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("無效的帳號或密碼");

            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
                return Unauthorized("無效的帳號或密碼");

            if (user is ApplicationUser applicationUser)
            {
                applicationUser.LastLogin = DateTime.UtcNow;
            }

            await userManager.UpdateAsync(user);

            return Ok(new { Message = "登入成功" });
        }

        [HttpPost("{userId}/roles")]
        public async Task<IActionResult> SetRoles(string userId, [FromBody] string[] roles)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);
            await userManager.AddToRolesAsync(user, roles);

            return Ok();
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = userManager.Users.ToList();

            var result = new List<object>();
            foreach (var user in users)
            {
                var applicationUser = user as ApplicationUser;
                var roles = await userManager.GetRolesAsync(user);
                result.Add(new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    Roles = roles,
                    applicationUser?.CreatedAt,
                    applicationUser?.LastLogin
                });
            }

            return Ok(result);
        }
    }
}
