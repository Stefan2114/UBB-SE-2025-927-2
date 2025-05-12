namespace AppCommonClasses.Interfaces
{
    public interface IDietaryPreferencesRepository
    {
        [Obsolete]
        void AddAllergyAndDietaryPreference(string username, string dietaryPreference, string allergy);
    }
}
