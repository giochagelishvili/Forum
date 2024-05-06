using Forum.Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.InteropServices;

namespace Forum.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(comment => comment.Id);

            builder.Property(comment => comment.Body)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(comment => comment.CreatedAt)
                .IsRequired();

            builder.Property(comment => comment.ModifiedAt)
                .IsRequired();

            builder.Property(comment => comment.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasQueryFilter(comment => !comment.IsDeleted);

            builder.HasOne(comment => comment.Author)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
