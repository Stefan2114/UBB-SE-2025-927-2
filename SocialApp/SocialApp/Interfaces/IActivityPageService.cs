namespace MealPlannerProject.Interfaces.Services
{
    internal interface IActivityPageService
    {
        [System.Obsolete]
        void AddActivity(string firstName, string lastName, string activityDescription);
    }
}