namespace SocialApp.Interfaces
{
    public interface IBodyMetricService
    {
        void UpdateUserBodyMetrics(string firstName, string lastName, string weight, string height, string targetGoal);
    }

}
