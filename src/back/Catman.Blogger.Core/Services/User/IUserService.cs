namespace Catman.Blogger.Core.Services.User
{
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Services.Common;

    public interface IUserService
    {
        Task<Response<User>> RegisterAsync(RegisterUserRequest registerRequest);

        Task<Response<LoginUserResult>> LoginAsync(LoginUserRequest loginRequest);
    }
}
