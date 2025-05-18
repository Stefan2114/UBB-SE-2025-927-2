namespace SocialApp.Interfaces
{
    public interface IUserPageService
    {
        long UserHasAnAccount(string name);

        long InsertNewUser(string name, string password);
    }
}
