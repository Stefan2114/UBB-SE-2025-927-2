﻿namespace SocialApp.Interfaces
{
    public interface IDietaryPreferencesService
    {
        void AddAllergyAndDietaryPreference(string firstName, string lastName, string dietaryPreference, string allergens);
    }
}