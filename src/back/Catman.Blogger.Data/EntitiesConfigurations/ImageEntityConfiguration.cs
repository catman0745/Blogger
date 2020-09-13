namespace Catman.Blogger.Data.EntitiesConfigurations
{
    using Catman.Blogger.Core.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("images");

            // unique image filename
            builder
                .HasIndex(image => image.FileName)
                .IsUnique();
            
            // user-images one-to-many relationship
            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(image => image.OwnerUsername);

            builder
                .Property(image => image.Id)
                .HasColumnName("id");

            builder
                .Property(image => image.FileName)
                .HasColumnName("file_name")
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(image => image.ContentType)
                .HasColumnName("content_type")
                .HasMaxLength(127)
                .IsRequired();

            builder
                .Property(image => image.OwnerUsername)
                .HasColumnName("owner")
                .IsRequired();
        }
    }
}
