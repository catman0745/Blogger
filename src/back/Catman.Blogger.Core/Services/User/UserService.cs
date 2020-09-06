namespace Catman.Blogger.Core.Services.User
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Helpers.Auth;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Catman.Blogger.Core.Services.Common;

    public class UserService : Service, IUserService
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public UserService(IUserRepository users, IUnitOfWork unitOfWork, ITokenHelper tokenHelper, IMapper mapper)
        {
            _users = users;
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }
        
        public async Task<Response<User>> RegisterAsync(RegisterUserRequest registerRequest)
        {
            if (await _users.ExistsAsync(registerRequest.Username))
            {
                return Failure<User>("User with such username already exists");
            }

            var user = _mapper.Map<User>(registerRequest);
            _users.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return Success(user);
        }

        public async Task<Response<LoginUserResult>> LoginAsync(LoginUserRequest loginRequest)
        {
            if (!await _users.ExistsAsync(loginRequest.Username))
            {
                return Failure<LoginUserResult>("User with such username does not exist");
            }

            var user = await _users.GetAsync(loginRequest.Username);
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
