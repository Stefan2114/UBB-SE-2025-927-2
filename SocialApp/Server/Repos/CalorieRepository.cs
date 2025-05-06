using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Server.Data;
using System.Linq;

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

    public void SetDailyIntake(long userId, double intake)
    {
        var calorie = GetCaloriesByUserId(userId);
        if (calorie != null)
        {
            calorie.DailyIntake = intake;
            this.dbContext.SaveChanges();
        }
    }

    public void SetCaloriesConsumed(long userId, double consumed)
    {
        var calorie = GetCaloriesByUserId(userId);
        if (calorie != null)
        {
            calorie.CaloriesConsumed = consumed;
            this.dbContext.SaveChanges();
        }
    }

    public void SetCaloriesBurned(long userId, double burned)
    {
        var calorie = GetCaloriesByUserId(userId);
        if (calorie != null)
        {
            calorie.CaloriesBurned = burned;
            this.dbContext.SaveChanges();
        }
    }
}
