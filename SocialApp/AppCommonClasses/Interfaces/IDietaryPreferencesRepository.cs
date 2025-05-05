namespace AppCommonClasses.Interfaces
{
    public interface IDietaryPreferencesRepository
    {
        void AddUserDietaryPreferenceIfNotExists(long userId, string dietaryPreference);
        string GetUserDietaryPreference(long userId);
        void UpdateUserDietaryPreference(long userId, string newDietaryPreference);
        void RemoveUserDietaryPreference(long userId);
    }
}
