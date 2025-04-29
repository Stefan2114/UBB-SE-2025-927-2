namespace AppCommonClasses.Interfaces
{
    public interface IDietaryPreferencesRepository
    {
        [Obsolete]
        void AddAllergyAndDietaryPreference(string firstName, string lastName, string dietaryPreference, string allergy);
    }
}
