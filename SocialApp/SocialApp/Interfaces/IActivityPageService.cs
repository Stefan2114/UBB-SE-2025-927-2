namespace SocialApp.Interfaces
{
    internal interface IActivityPageService
    {
        [System.Obsolete]
        void AddActivity(string username, string activityDescription);
    }
}