namespace Catman.Blogger.Core
{
    using Catman.Blogger.Core.Models;
    using Microsoft.EntityFrameworkCore;

    public class BloggerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Blog> Blogs { get; set; }
        
        public DbSet<Post> Posts { get; set; }
        
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

            // unique post title for each blog
            modelBuilder
                .Entity<Post>()
                .HasIndex(post => new {post.Title, post.BlogId})
                .IsUnique();
        }
    }
}
