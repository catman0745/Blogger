namespace Catman.Blogger.Data.EntitiesConfigurations
{
    using Catman.Blogger.Core.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("posts");
            
            // unique post title per blog
            builder
                .HasIndex(post => new {post.Title, post.BlogId})
                .IsUnique();
            
            // blog-posts one-to-many relationship
            builder
                .HasOne<Blog>()
                .WithMany()
                .HasForeignKey(post => post.BlogId);

            builder
                .Property(post => post.Id)
                .HasColumnName("id");

            builder
                .Property(post => post.Title)
                .HasColumnName("title")
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(post => post.Content)
                .HasColumnName("content")
                .IsRequired();

            builder
                .Property(post => post.CreatedAt)
                .HasColumnName("created_at");

            builder
                .Property(post => post.LastUpdate)
                .HasColumnName("last_update");

            builder
                .Property(post => post.BlogId)
                .HasColumnName("blog_id");
        }
    }
}
