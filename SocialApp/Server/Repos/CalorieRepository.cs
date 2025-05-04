using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Server.Data;

public class CalorieRepository : ICalorieRepository
{
    private readonly SocialAppDbContext dbContext;

    public CalorieRepository(SocialAppDbContext context)
    {
        this.dbContext = context;
    }

    public Calorie GetCaloriesByUserId(long userId)
    {
        return this.dbContext.Calories.FirstOrDefault(c => c.U_Id == userId);
    }
}
