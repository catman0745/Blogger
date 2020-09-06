namespace Catman.Blogger.Core.Services.User
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Helpers.Auth;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Common;
    using Microsoft.EntityFrameworkCore;

    public class UserService : Service, IUserService
    {
        private readonly BloggerDbContext _context;
        private readonly TokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public UserService(BloggerDbContext context, TokenHelper tokenHelper, IMapper mapper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }
        
        public async Task<Response<User>> RegisterAsync(RegisterUserRequest registerRequest)
        {
            if (await _context.Users.AnyAsync(u => u.Username == registerRequest.Username))
            {
                return Failure<User>("User with such username already exists");
            }

            var user = _mapper.Map<User>(registerRequest);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Success(user);
        }

        public async Task<Response<LoginUserResult>> LoginAsync(LoginUserRequest loginRequest)
        {
            if (!await _context.Users.AnyAsync(u => u.Username == loginRequest.Username))
            {
                return Failure<LoginUserResult>("User with such username doest not exist");
            }

            var user = await _context.Users.SingleAsync(u => u.Username == loginRequest.Username);
            if (user.Password != loginRequest.Password)
            {
                return Failure<LoginUserResult>("Incorrect password");
            }

            var loginResult = _mapper.Map<LoginUserResult>(user);
            loginResult.Token = _tokenHelper.GenerateToken(user);
            return Success(loginResult);
        }
    }
}
