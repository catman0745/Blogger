namespace Catman.Blogger.API.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.API.Auth;
    using Catman.Blogger.API.Data;
    using Catman.Blogger.API.DataTransferObjects.User;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly BloggerDbContext _context;
        private readonly TokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public UserController(BloggerDbContext context, TokenHelper tokenHelper, IMapper mapper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                return BadRequest("User with such username already exists");
            }

            _context.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null)
            {
                return NotFound("User with such username does not exist");
            }
            if (user.Password != loginDto.Password)
            {
                return BadRequest("Incorrect password was provided");
            }

            var loggedDto = _mapper.Map<UserLoggedDto>(user);
            loggedDto.Token = _tokenHelper.GenerateToken(user);
            return Ok(loggedDto);
        }
    }
}
