using AppCommonClasses.Models;

namespace AppCommonClasses.Interfaces
{
    public interface ICalorieRepository
    {
        Calorie? GetCaloriesByUserId(long userId);
    }
}