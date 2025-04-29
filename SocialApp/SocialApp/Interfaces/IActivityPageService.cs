namespace SocialApp.Interfaces
{
    internal interface IActivityPageService
    {
        [System.Obsolete]
        void AddActivity(string firstName, string lastName, string activityDescription);
    }
}