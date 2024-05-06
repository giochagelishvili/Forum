using Forum.Domain.Roles;
using Forum.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.UserName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(user => user.NormalizedUserName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(user => user.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(user => user.NormalizedEmail)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(user => user.IsBanned)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(user => user.PasswordHash)
                .IsRequired();

            builder.Property(user => user.CreatedAt)
                .IsRequired();

            builder.Property(user => user.ModifiedAt)
                .IsRequired();

            // Ignored properties
            builder.Ignore(user => user.EmailConfirmed);

            builder.Ignore(user => user.ConcurrencyStamp);

            builder.Ignore(user => user.PhoneNumber);

            builder.Ignore(user => user.PhoneNumberConfirmed);

            builder.Ignore(user => user.TwoFactorEnabled);

            builder.Ignore(user => user.LockoutEnabled);

            builder.Ignore(user => user.LockoutEnd);

            builder.Ignore(user => user.AccessFailedCount);
        }
    }
}
