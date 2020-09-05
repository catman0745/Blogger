namespace Catman.Blogger.API.Data
{
    using Microsoft.EntityFrameworkCore;

    public class BloggerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Blog> Blogs { get; set; }
        
        public BloggerDbContext(DbContextOptions<BloggerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // unique blog name
            modelBuilder
                .Entity<Blog>()
                .HasIndex(blog => blog.Name)
                .IsUnique();
        }
    }
}
