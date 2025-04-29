namespace MealPlannerProject.Exceptions
{
    using System;

    public class ConfigurationErrorsException : Exception
    {
        public ConfigurationErrorsException()
        {
        }

        public ConfigurationErrorsException(string message)
            : base(message)
        {
        }

        public ConfigurationErrorsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
