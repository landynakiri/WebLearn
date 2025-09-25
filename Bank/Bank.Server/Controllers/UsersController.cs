using Bank.Server.Data;
using Bank.Server.Models;
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
        private readonly SignInManager<IdentityUser> signInManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
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
        public async Task<ActionResult<LoginResp>> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
                return Unauthorized("無效的帳號或密碼");

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("無效的帳號或密碼");

            if (user is ApplicationUser applicationUser)
            {
                applicationUser.LastLogin = DateTime.UtcNow;
            }

            await userManager.UpdateAsync(user);

            var currentRoles = await userManager.GetRolesAsync(user);

            return Ok(new LoginResp
            {
                Roles = currentRoles
            });
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
        public async Task<ActionResult<IEnumerable<GetUserResp>>> GetUsers()
        {
            var users = userManager.Users.ToList();

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

            return Ok(result);
        }

        [HttpGet("GetTest")]
        public async Task<ActionResult<IList<string>>> GetTest()
        {
            

            return Ok(new List<string> { "asdf"});
        }
    }
}
