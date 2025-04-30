namespace SocialApp.Interfaces
{
    public interface IUserPageService
    {
        int UserHasAnAccount(string name);
        int InsertNewUser(string name);
    }
}
