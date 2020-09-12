namespace Catman.Blogger.Data.EntitiesConfigurations
{
    using Catman.Blogger.Core.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(user => user.Username);

            builder
                .Property(user => user.Username)
                .HasColumnName("username")
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(user => user.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(user => user.Password)
                .HasColumnName("password")
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(user => user.AvatarUrl)
                .HasColumnName("avatar")
                .HasMaxLength(2084);
        }
    }
}
