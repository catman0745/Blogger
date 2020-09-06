namespace Catman.Blogger.Core.Repositories
{
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;

    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string username);

        Task<User> GetAsync(string username);

        void Add(User user);
    }
}
