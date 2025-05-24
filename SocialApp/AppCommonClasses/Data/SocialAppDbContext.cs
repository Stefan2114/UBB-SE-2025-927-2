namespace AppCommonClasses.Data
{
    using AppCommonClasses.DbRelationshipEntities;
    using AppCommonClasses.Models;
    using Microsoft.EntityFrameworkCore;

    public class SocialAppDbContext : DbContext
    {
        public SocialAppDbContext(DbContextOptions<SocialAppDbContext> options)
            : base(options)
        {
        }
        // not used anymore. Use the field waterconsumed from the user 
        public DbSet<Water> WaterTrackers { get; set; } = default!;

        public DbSet<Post> Posts { get; set; } = default!;

        public DbSet<Comment> Comments { get; set; } = default!;

        public DbSet<UserFollower> UserFollowers { get; set; } = default!;

        public DbSet<GroupUser> GroupUsers { get; set; } = default!;

        public DbSet<Group> Groups { get; set; } = default!;

        public DbSet<GroceryIngredient> GroceryIngredients { get; set; } = default!;

        public DbSet<Ingredient> Ingredient { get; set; } = default!;

        public DbSet<Reaction> Reactions { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<Calorie> Calories { get; set; } = default!;

        public DbSet<Meal> Meals { get; set; } = default!;

        public DbSet<MealIngredient> MealIngredients { get; set; } = default!;


        public DbSet<Ingredient> Ingredients { get; set; } = default!;

        public DbSet<Macros> Macros { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupUser>()
                .HasKey(groupUser => new { groupUser.UserId, groupUser.GroupId });

            modelBuilder.Entity<UserFollower>()
                .HasKey(userFollower => new { userFollower.UserId, userFollower.FollowerId });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", tableBuilder =>
                {
                    tableBuilder.HasCheckConstraint("CK_User_Height_Positive", "[height] > 0");
                    tableBuilder.HasCheckConstraint("CK_User_Weight_Positive", "[weight] > 0");
                    tableBuilder.HasCheckConstraint("CK_User_Water_Consumed_Positive", "[water_consumed] >= 0");
                    tableBuilder.HasCheckConstraint("CK_User_Goal_Valid", "[goal] IN ('lose weight', 'maintain', 'gain muscle')");
                    tableBuilder.HasCheckConstraint("CK_User_Gender_Valid", "[gender] IN ('male', 'female')");
                    tableBuilder.HasCheckConstraint("CK_User_Activity_Valid", "[activity_level] IN ('sedentarty', 'light', 'moderate', 'high', 'extra')");


                });

                entity.HasIndex(user => user.Username).IsUnique();
            });
               

            modelBuilder.Entity<GroceryIngredient>()
            .HasKey(groceryIngredient => new { groceryIngredient.Id, groceryIngredient.IngredientId });

            modelBuilder.Entity<Macros>()
                .HasKey(m => new { m.DailyMealId});
            modelBuilder.Entity<Ingredient>().HasKey(ingredient => new { ingredient.Id });

            modelBuilder.Entity<Post>()
                .Property(post => post.Visibility)
                .HasConversion<int>();

            modelBuilder.Entity<Calorie>()
                .HasOne(c => c.User)
                .WithMany() // Assuming a one-to-many relationship from User to Calorie
                .HasForeignKey(c => c.U_Id);

            modelBuilder.Entity<Reaction>()
                .HasKey(reaction => new { reaction.UserId, reaction.PostId });

            modelBuilder.Entity<Reaction>()
                .Property(reaction => reaction.Type)
                .HasColumnName("ReactionType");
        }
    }
}