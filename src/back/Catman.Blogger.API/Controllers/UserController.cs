namespace Catman.Blogger.API.Controllers
{
    using System.Threading.Tasks;
    using Catman.Blogger.API.Auth;
    using Catman.Blogger.API.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly BloggerDbContext _context;
        private readonly TokenHelper _tokenHelper;

        public UserController(BloggerDbContext context, TokenHelper tokenHelper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                return BadRequest("User with such username already exists");
            }

            _context.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            if (!await _context.Users.AnyAsync(u => u.Username == user.Username && u.Password == user.Password))
            {
                return BadRequest("Incorrect username or password");
            }

            var token = _tokenHelper.GenerateToken(user);
            return Ok(token);
        }
    }
}
