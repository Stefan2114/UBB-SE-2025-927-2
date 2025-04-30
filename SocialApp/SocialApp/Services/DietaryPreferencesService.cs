namespace SocialApp.Services
{
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using SocialApp.Interfaces;
    using SocialApp.Repository;
    using System;

    public class DietaryPreferencesService : IDietaryPreferencesService
    {
        private readonly IDietaryPreferencesRepository repository;

        public DietaryPreferencesService()
        {
            repository = new DietaryPreferencesRepository();
        }

        [Obsolete]
        public void AddAllergyAndDietaryPreference(string firstName, string lastName, string dietaryPreference, string allergy)
        {
            if (dietaryPreference == DietaryPreferenceType.NotSelected.ToString() && allergy == DietaryPreferenceType.NotSelected.ToString())
            {
                throw new Exception("Please select a dietary preference and allergies!");
            }

            if (dietaryPreference == DietaryPreferenceType.NotSelected.ToString())
            {
                throw new Exception("Please select a dietary preference!");
            }

            if (allergy == DietaryPreferenceType.NotSelected.ToString())
            {
                throw new Exception("Please select allergies!");
            }

            repository.AddAllergyAndDietaryPreference(firstName, lastName, dietaryPreference, allergy);
        }
    }
}
