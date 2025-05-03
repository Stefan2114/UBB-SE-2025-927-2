using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommonClasses.Enums;

namespace AppCommonClasses.Models
{

    public class Meal
    {
        [Key]
        [Column("m_id")]
        public int Id { get; set; }

        [Column("m_name")]
        public string Name { get; set; }

        public string Ingredients { get; set; }

        [Column("calories")]
        public int Calories { get; set; }


        public string Category { get; set; }

        public int Protein { get; set; }

        public int Carbohydrates { get; set; }

        public int Fat { get; set; }

        public int Fiber { get; set; }

        public int Sugar { get; set; }

        [Column("photo_link")]
        public string PhotoLink { get; set; }

        public string Recipe { get; set; }

        [Column("preparation_time")]
        public double PreparationTime { get; set; }

        public double Servings { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CookingLevel { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }

        public Meal(string name, string ingredients, int calories, string category, string photoLink, string recipe)
        {
            Name = name;
            Ingredients = ingredients;
            Calories = calories;
            Category = category;
            PhotoLink = photoLink;
            Recipe = recipe;
            CreatedAt = DateTime.Now;
        }

        public Meal()
        {
            CreatedAt = DateTime.Now;
        }
    }

    public enum MealModel
    {
        SuccessfulCreationIndicator = 0,
        FailedOperationCode = -1,
        BreakfastTypeId = 1,
        LunchTypeId = 2,
        DinnerTypeId = 3,
        SnackTypeId = 4,
        DessertTypeId = 5,
        PostWorkoutTypeId = 6,
        PreWorkoutTypeId = 7,
        VeganMealTypeId = 8,
        HighProteinMealTypeId = 9,
        LowCarbMealTypeId = 10,
        DefaultMealTypeId = 1,
        BeginnerSkillId = 1,
        IntermediateSkillId = 2,
        AdvancedSkillId = 3,
        DefaultCookingSkillId = 1,
    }
}