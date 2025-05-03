using Server.DbRelationshipEntities;
using AppCommonClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class SocialAppDbContext : DbContext
    {
        public SocialAppDbContext(DbContextOptions<SocialAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = default!;

        public DbSet<UserFollower> UserFollowers { get; set; } = default!;

        public DbSet<GroupUser> GroupUsers { get; set; } = default!;

        public DbSet<UserModel> Users { get; set; } = default!;

        public DbSet<Goal> Goals { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupUser>()
                .HasKey(groupUser => new { groupUser.UserId, groupUser.GroupId });
            modelBuilder.Entity<UserFollower>()
                .HasKey(userFollower => new { userFollower.UserId, userFollower.FollowerId });

            modelBuilder.Entity<Post>()
                .Property(post => post.Visibility)
                .HasConversion<int>();
        }
    }
}
