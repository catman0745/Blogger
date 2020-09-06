namespace Catman.Blogger.Data.Repositories
{
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Core.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly BloggerDbContext _context;

        public UserRepository(BloggerDbContext context)
        {
            _context = context;
        }
        
        public Task<bool> ExistsAsync(string username)
        {
            return _context.Users.AnyAsync(user => user.Username == username);
        }

        public Task<User> GetAsync(string username)
        {
            return _context.Users.AsNoTracking().SingleAsync(user => user.Username == username);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }
    }
}
