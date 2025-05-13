namespace AppCommonClasses.Repos
{
    using AppCommonClasses.Data;
    using AppCommonClasses.Interfaces;

    public class BodyMetricRepository : IBodyMetricRepository
    {
        private readonly SocialAppDbContext dbContext;

        public BodyMetricRepository(SocialAppDbContext context)
        {
            dbContext = context;
        }

        public void UpdateUserBodyMetrics(long userId, float weight, float height, float? TargetWeight)
        {
            var user = dbContext.Users.Find(userId);
            if (user != null)
            {
                user.Weight = weight;
                user.Height = height;
                user.TargetWeight = TargetWeight;
                dbContext.SaveChanges();
            }
        }
    }
}