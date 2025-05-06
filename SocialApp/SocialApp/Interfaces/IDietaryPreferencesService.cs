namespace SocialApp.Interfaces
{
    public interface IDietaryPreferencesService
    {
        void AddAllergyAndDietaryPreference(string username, string dietaryPreference, string allergens);
    }
}