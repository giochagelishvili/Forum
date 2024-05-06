using Forum.Domain.BaseEntities;
using Microsoft.AspNetCore.Identity;

namespace Forum.Domain.Roles
{
    public class Role : IdentityRole<int>, IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
