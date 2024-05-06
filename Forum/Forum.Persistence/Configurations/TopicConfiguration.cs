using Forum.Domain;
using Forum.Domain.Topics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(topic => topic.Id);

            builder.Property(topic => topic.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(topic => topic.Description)
                .IsRequired();

            builder.Property(topic => topic.CreatedAt)
                .IsRequired();

            builder.Property(topic => topic.ModifiedAt)
                .IsRequired();

            builder.Property(topic => topic.Status)
                .IsRequired()
                .HasDefaultValue(Status.Active);

            builder.Property(topic => topic.State)
                .IsRequired()
                .HasDefaultValue(State.Pending);

            // Navigation properties
            builder.HasOne(topic => topic.Author)
                .WithMany(user => user.Topics)
                .HasForeignKey(topic => topic.AuthorId);

            builder.HasMany(topic => topic.Comments)
                .WithOne(comment => comment.Topic)
                .HasForeignKey(comment => comment.TopicId);
        }
    }
}
