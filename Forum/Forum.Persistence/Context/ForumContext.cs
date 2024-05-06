using Forum.Domain.BaseEntities;
using Forum.Domain.Comments;
using Forum.Domain.Images;
using Forum.Domain.Roles;
using Forum.Domain.Topics;
using Forum.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Context
{
    public class ForumContext : IdentityDbContext<User, Role, int>
    {
        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<IdentityUserToken<int>>();
            modelBuilder.Ignore<IdentityUserLogin<int>>();

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");

            modelBuilder.Entity<Image>().ToTable("Images");
            modelBuilder.Entity<Topic>().ToTable("Topics");
            modelBuilder.Entity<Comment>().ToTable("Comments");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForumContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var trackedEntries = base.ChangeTracker.Entries<IEntity>()
                                     .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified);

            foreach (var entry in trackedEntries)
            {
                entry.Entity.ModifiedAt = DateTime.Now.ToUniversalTime();

                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = DateTime.Now.ToUniversalTime();
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
