using AppCommonClasses.Models;
using Microsoft.EntityFrameworkCore;
using Server.DbRelationshipEntities;

namespace Server.Data
{
    public class SocialAppDbContext : DbContext
    {
        public SocialAppDbContext(DbContextOptions<SocialAppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Water> WaterTrackers { get; set; } = default!;
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;

        public DbSet<UserFollower> UserFollowers { get; set; } = default!;

        public DbSet<GroupUser> GroupUsers { get; set; } = default!;

        public DbSet<Group> Groups { get; set; } = default!;

        public DbSet<Reaction> Reactions { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Calorie> Calories { get; set; } = default!; 
        public DbSet<Meal> Meals { get; set; } = default!;
        public DbSet<MealIngredient> MealIngredients { get; set; } = default!;

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

            modelBuilder.Entity<Calorie>()
                .HasOne(c => c.User)
                .WithMany()  // Assuming a one-to-many relationship from User to Calorie
                .HasForeignKey(c => c.U_Id);

            modelBuilder.Entity<Reaction>()
                .HasKey(reaction => new { reaction.UserId, reaction.PostId });

            modelBuilder.Entity<Reaction>()
                .Property(reaction => reaction.Type)
                .HasColumnName("ReactionType");
        }
    }
}