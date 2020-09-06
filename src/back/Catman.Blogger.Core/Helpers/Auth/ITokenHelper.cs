namespace Catman.Blogger.Core.Helpers.Auth
{
    using Catman.Blogger.Core.Models;

    public interface ITokenHelper
    {
        string GenerateToken(User user);
    }
}
