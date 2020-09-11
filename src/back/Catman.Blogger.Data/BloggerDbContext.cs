namespace Catman.Blogger.Data
{
    using Catman.Blogger.Core.Models;
    using Catman.Blogger.Data.EntitiesConfigurations;
    using Microsoft.EntityFrameworkCore;
    
    public class BloggerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Blog> Blogs { get; set; }
        
        public DbSet<Post> Posts { get; set; }
        
        public DbSet<Image> Images { get; set; }
        
        public BloggerDbContext(DbContextOptions<BloggerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BlogEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ImageEntityConfiguration());
        }
    }
}
