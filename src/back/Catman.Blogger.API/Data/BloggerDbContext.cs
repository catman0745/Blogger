namespace Catman.Blogger.API.Data
{
    using Microsoft.EntityFrameworkCore;

    public class BloggerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public BloggerDbContext(DbContextOptions<BloggerDbContext> options)
            : base(options)
        {
        }
    }
}
