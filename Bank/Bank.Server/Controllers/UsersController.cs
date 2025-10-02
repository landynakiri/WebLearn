using Bank.Server.Data;
using Bank.Server.Services;
using Bank.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly UserService userService;

        public UsersController(AuthService authService, UserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.RegisterAsync(model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResp>> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.LoginAsync(model);
            if (result == null)
                return Unauthorized("無效的帳號或密碼");
            else
                return Ok(result);
        }

        [HttpPost("{userId}/roles")]
        public async Task<IActionResult> SetRoles(string userId, [FromBody] string[] roles)
        {
            bool result = await userService.SetRolesAsync(userId, roles);

            if(!result)
                return NotFound();
            else
                return Ok();
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<GetUserResp>>> GetUsers()
        {
            var result = await userService.GetUsers();

            if(result == null || !result.Any())
                return NotFound();
            else
                return Ok(result);
        }
    }
}
