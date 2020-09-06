namespace Catman.Blogger.Data.Repositories
{
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BloggerDbContext _context;

        public UnitOfWork(BloggerDbContext context)
        {
            _context = context;
        }
        
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
