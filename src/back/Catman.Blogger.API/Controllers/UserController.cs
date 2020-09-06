namespace Catman.Blogger.API.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.API.DataTransferObjects.User;
    using Catman.Blogger.Core.Services.User;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _users;
        private readonly IMapper _mapper;

        public UserController(IUserService users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequestDto registerRequestDto)
        {
            var registerRequest = _mapper.Map<RegisterUserRequest>(registerRequestDto);
            
            var response = await _users.RegisterAsync(registerRequest);
            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequestDto loginRequestDto)
        {
            var loginRequest = _mapper.Map<LoginUserRequest>(loginRequestDto);

            var result = await _users.LoginAsync(loginRequest);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            var loggedDto = _mapper.Map<LoginUserResultDto>(result.Result);
            return Ok(loggedDto);
        }
    }
}
