using Forum.Domain.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(image => image.Url)
                .IsRequired();

            builder.HasOne(image => image.User)
                .WithOne(user => user.Image)
                .HasForeignKey<Image>(image => image.UserId);
        }
    }
}
