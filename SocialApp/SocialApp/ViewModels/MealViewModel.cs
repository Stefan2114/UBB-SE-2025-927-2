namespace SocialApp.ViewModels
{
    using AppCommonClasses.Models;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;

    public class MealViewModel : INotifyPropertyChanged
    {
        private readonly MealCreate model;

        public string MealName
        {
            get => model.MealName;
            set
            {
                if (model.MealName != value)
                {
                    model.MealName = value;
                    Debug.WriteLine($"MealName set to: {value}");
                    OnPropertyChanged(nameof(model.MealName));
                }
            }
        }

        public string CookingTime
        {
            get => model.CookingTime;
            set
            {
                if (model.CookingTime != value)
                {
                    model.CookingTime = value;
                    Debug.WriteLine($"CookingTime set to: {value}");
                    OnPropertyChanged(nameof(model.CookingTime));
                }
            }
        }

        public ObservableCollection<string> CookingDirections
        {
            get => model.CookingDirections;
            set
            {
                if (model.CookingDirections != value)
                {
                    model.CookingDirections = value;
                    Debug.WriteLine($"Directions set with {value?.Count ?? 0} items");
                    OnPropertyChanged(nameof(model.CookingDirections));
                }
            }
        }

        public ObservableCollection<string> MealIngredients
        {
            get => model.MealIngredients;
            set
            {
                if (model.MealIngredients != value)
                {
                    model.MealIngredients = value;
                    Debug.WriteLine($"Ingredients set with {value?.Count ?? 0} items");
                    OnPropertyChanged(nameof(model.MealIngredients));
                }
            }
        }

        public int CalorieCount
        {
            get => model.CalorieCount;
            set
            {
                if (model.CalorieCount != value)
                {
                    model.CalorieCount = value;
                    Debug.WriteLine($"Calories set to: {value}");
                    OnPropertyChanged(nameof(model.CalorieCount));
                }
            }
        }

        public int ProteinGrams
        {
            get => model.ProteinGrams;
            set
            {
                if (model.ProteinGrams != value)
                {
                    model.ProteinGrams = value;
                    Debug.WriteLine($"Protein set to: {value}");
                    OnPropertyChanged(nameof(model.ProteinGrams));
                }
            }
        }

        public int CarbohydrateGrams
        {
            get => model.CarbohydrateGrams;
            set
            {
                if (model.CarbohydrateGrams != value)
                {
                    model.CarbohydrateGrams = value;
                    Debug.WriteLine($"Carbs set to: {value}");
                    OnPropertyChanged(nameof(model.CarbohydrateGrams));
                }
            }
        }

        public int FatGrams
        {
            get => model.FatGrams;
            set
            {
                if (model.FatGrams != value)
                {
                    model.FatGrams = value;
                    Debug.WriteLine($"Fat set to: {value}");
                    OnPropertyChanged(nameof(model.FatGrams));
                }
            }
        }

        public int FiberGrams
        {
            get => model.FiberGrams;
            set
            {
                if (model.FiberGrams != value)
                {
                    model.FiberGrams = value;
                    Debug.WriteLine($"Fiber set to: {value}");
                    OnPropertyChanged(nameof(model.FiberGrams));
                }
            }
        }

        public int SugarGrams
        {
            get => model.SugarGrams;
            set
            {
                if (model.SugarGrams != value)
                {
                    model.SugarGrams = value;
                    Debug.WriteLine($"Sugar set to: {value}");
                    OnPropertyChanged(nameof(model.SugarGrams));
                }
            }
        }

        public MealViewModel()
        {
            model = new MealCreate();
            Debug.WriteLine("Initializing MealViewModel");
        }

        public void InitializeFromMeal(Meal meal)
        {
            Debug.WriteLine($"Initializing MealViewModel from Meal: {meal?.Name ?? "null"}");

            if (meal == null)
            {
                Debug.WriteLine("Warning: Meal object is null");
                return;
            }

            MealName = meal.Name;
            CookingTime = $"{meal.PreparationTime} min";
            CalorieCount = (int)meal.Calories;
            ProteinGrams = (int)meal.Protein;
            CarbohydrateGrams = (int)meal.Carbohydrates;
            FatGrams = (int)meal.Fat;
            FiberGrams = (int)meal.Fiber;
            SugarGrams = (int)meal.Sugar;

            if (!string.IsNullOrEmpty(meal.Ingredients))
            {
                MealIngredients = new ObservableCollection<string>(meal.Ingredients.Split('\n'));
                Debug.WriteLine($"Loaded {MealIngredients.Count} ingredients");
            }
            else
            {
                Debug.WriteLine("No ingredients found in meal");
                MealIngredients = new ObservableCollection<string> { "No ingredients available" };
            }

            if (!string.IsNullOrEmpty(meal.Recipe))
            {
                CookingDirections = new ObservableCollection<string>(meal.Recipe.Split('\n'));
                Debug.WriteLine($"Loaded {CookingDirections.Count} directions");
            }
            else
            {
                Debug.WriteLine("No recipe found in meal");
                CookingDirections = new ObservableCollection<string> { "No directions available" };
            }

            Debug.WriteLine("Finished initializing MealViewModel from Meal");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
