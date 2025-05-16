namespace SocialApp.Interfaces
{
    public interface IBodyMetricService
    {
        void UpdateUserBodyMetrics(string username, string weight, string height, string targetGoal);
    }
}
