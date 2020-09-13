namespace Catman.Blogger.Data.EntitiesConfigurations
{
    using Catman.Blogger.Core.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class BlogEntityConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("blogs");

            // unique blog name
            builder
                .HasIndex(blog => blog.Name)
                .IsUnique();

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(blog => blog.OwnerUsername);

            builder
                .Property(blog => blog.Id)
                .HasColumnName("id");
            
            builder
                .Property(blog => blog.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(blog => blog.CreatedAt)
                .HasColumnName("created_at");

            builder
                .Property(blog => blog.OwnerUsername)
                .HasColumnName("owner")
                .IsRequired();
        }
    }
}
