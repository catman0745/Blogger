namespace Catman.Blogger.Data.EntitiesConfigurations
{
    using Catman.Blogger.Core.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");

            // user-comments one-to-many relationship
            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(comment => comment.OwnerUsername);
            
            // post-comment one-to-many relationship
            builder
                .HasOne<Post>()
                .WithMany()
                .HasForeignKey(comment => comment.PostId);
            
            builder
                .Property(comment => comment.Id)
                .HasColumnName("id");

            builder
                .Property(comment => comment.Content)
                .HasColumnName("content")
                .HasMaxLength(500)
                .IsRequired();

            builder
                .Property(comment => comment.CreatedAt)
                .HasColumnName("created_at");

            builder
                .Property(comment => comment.OwnerUsername)
                .HasColumnName("owner")
                .IsRequired();

            builder
                .Property(comment => comment.PostId)
                .HasColumnName("post_id");
        }
    }
}
