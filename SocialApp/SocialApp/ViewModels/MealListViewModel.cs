using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Data;
using System.Data.SqlClient;
using MealPlannerProject.Models;
using MealPlannerProject.Services;
using MealPlannerProject.Queries;
using System.Diagnostics;
using MealPlannerProject.Pages;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

using MealPlannerProject.Interfaces;

namespace MealPlannerProject.ViewModels
{
    public class MealListViewModel : ViewModelBase, IMealListViewModel
    {
        private readonly NavigationService _navigationService;
        private Meal _selectedMeal;
        private MealService _mealService;
        public ObservableCollection<Meal> AllMeals { get; }
        public ObservableCollection<Meal> BreakfastMeals { get; private set; }
        public ObservableCollection<Meal> LunchMeals { get; private set; }
        public ObservableCollection<Meal> DinnerMeals { get; private set; }
        public ObservableCollection<Meal> SnackMeals { get; private set; }
        public ObservableCollection<string> RecentMeals { get; }
        public ObservableCollection<string> FavoriteMeals { get; }

        public ICommand BackCommand { get; }
        public ICommand SelectMealCommand { get; }

        public Meal SelectedMeal
        {
            get => _selectedMeal;
            set
            {
                if (_selectedMeal != value)
                {
                    _selectedMeal = value;
                    OnPropertyChanged();
                }
            }
        }

        // Parameterless constructor for XAML compatibility
        public MealListViewModel()
        {
            AllMeals = new ObservableCollection<Meal>();
            BreakfastMeals = new ObservableCollection<Meal>();
            LunchMeals = new ObservableCollection<Meal>();
            DinnerMeals = new ObservableCollection<Meal>();
            SnackMeals = new ObservableCollection<Meal>();
            RecentMeals = new ObservableCollection<string>();
            FavoriteMeals = new ObservableCollection<string>();
        }

        // Constructor with MealService dependency
        public MealListViewModel(MealService mealService) : this()
        {
            _mealService = mealService;
            Debug.WriteLine("Initializing MealListViewModel...");
            _navigationService = NavigationService.Instance;

            // Initialize commands
            BackCommand = new RelayCommand(NavigateBack);
            SelectMealCommand = new RelayCommand<Meal>(OnMealClicked);

            // Test database connection
            TestDatabaseConnection();

            // Load meals from database
            LoadMealsFromDatabase();

            // Initialize category-specific collections
            UpdateCategoryCollections();

            // Mock Recent Meals
            RecentMeals = new ObservableCollection<string>
            {
                "Grilled Chicken Salad (350 kcal)",
                "Vegetable Stir-Fry with Tofu (400 kcal)",
                "Lentil Soup (250 kcal)",
                "Veggie Omelette (300 kcal)",
                "Chickpea and Spinach Curry (368 kcal)",
                "Turkey and Avocado Wrap (421 kcal)",
                "Baked Cod with Asparagus (334 kcal)",
                "Butternut Squash Soup (224 kcal)"
            };

            // Mock Favorite Meals
            FavoriteMeals = new ObservableCollection<string>
            {
                "Cabbage and Carrot Slaw (128 kcal)",
                "Eggplant Parmesan (Baked) (356 kcal)",
                "Mango and Black Bean Salad (223 kcal)",
                "Moroccan Chickpea Stew (311 kcal)",
                "Roasted Cauliflower Tacos (358 kcal)",
                "Vegetable Paella (410 kcal)",
                "Grilled Pineapple Chicken (382 kcal)",
                "Stuffed Zucchini Boats (270 kcal)"
            };

            Debug.WriteLine($"MealListViewModel initialized with {AllMeals.Count} total meals");
            Debug.WriteLine($"Breakfast meals: {BreakfastMeals.Count}");
            Debug.WriteLine($"Lunch meals: {LunchMeals.Count}");
            Debug.WriteLine($"Dinner meals: {DinnerMeals.Count}");
            Debug.WriteLine($"Snack meals: {SnackMeals.Count}");
        }

        private void OnMealClicked(Meal meal)
        {
            if (meal != null)
            {
                SelectedMeal = meal;
                NavigateToMealDisplay();
            }
        }

        private async Task TestDatabaseConnection()
        {
            try
            {
                Debug.WriteLine("Testing database connection for meals...");

                // Test if GetMealsByCategory stored procedure exists
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@category", "Breakfast")
                };

                Debug.WriteLine("Testing GetMealsByCategory stored procedure...");
                var dataTable = DataLink.Instance.ExecuteReader("GetMealsByCategory", parameters);
                Debug.WriteLine($"Retrieved {dataTable.Rows.Count} breakfast meals from database");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database connection test failed: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private async Task LoadMealsFromDatabase()
        {
            try
            {
                Debug.WriteLine("Starting to load meals from database...");

                // Load meals for each category
                await LoadMealsByCategory("Breakfast");
                await LoadMealsByCategory("Lunch");
                await LoadMealsByCategory("Dinner");
                await LoadMealsByCategory("Snack");

                Debug.WriteLine($"Total meals loaded: {AllMeals.Count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading meals: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private async Task LoadMealsByCategory(string category)
        {
            try
            {
                Debug.WriteLine($"Loading {category} meals...");

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@category", category)
                };

                Debug.WriteLine($"Executing GetMealsByCategory for {category}...");
                var dataTable = DataLink.Instance.ExecuteReader("GetMealsByCategory", parameters);
                Debug.WriteLine($"Retrieved {dataTable.Rows.Count} {category} meals");

                foreach (DataRow row in dataTable.Rows)
                {
                    try
                    {
                        var meal = new Meal
                        {
                            Name = row["Name"].ToString(),
                            Recipe = row["Recipe"].ToString(),
                            Calories = Convert.ToInt32(row["Calories"]),
                            Category = row["Category"].ToString(),
                            Protein = Convert.ToInt32(row["Protein"]),
                            Carbohydrates = Convert.ToInt32(row["Carbohydrates"]),
                            Fat = Convert.ToInt32(row["Fat"]),
                            Fiber = Convert.ToInt32(row["Fiber"]),
                            Sugar = Convert.ToInt32(row["Sugar"]),
                            PhotoLink = row["PhotoLink"].ToString(),
                            PreparationTime = Convert.ToInt32(row["PreparationTime"]),
                            Servings = Convert.ToInt32(row["Servings"]),
                            ImagePath = row["PhotoLink"].ToString() // Using PhotoLink as ImagePath
                        };

                        AllMeals.Add(meal);
                        Debug.WriteLine($"Added meal: {meal.Name}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error processing meal row: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading {category} meals: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private void UpdateCategoryCollections()
        {
            Debug.WriteLine("Updating category collections...");

            BreakfastMeals = new ObservableCollection<Meal>(AllMeals.Where(m => m.Category == "Breakfast"));
            LunchMeals = new ObservableCollection<Meal>(AllMeals.Where(m => m.Category == "Lunch"));
            DinnerMeals = new ObservableCollection<Meal>(AllMeals.Where(m => m.Category == "Dinner"));
            SnackMeals = new ObservableCollection<Meal>(AllMeals.Where(m => m.Category == "Snack"));

            OnPropertyChanged(nameof(BreakfastMeals));
            OnPropertyChanged(nameof(LunchMeals));
            OnPropertyChanged(nameof(DinnerMeals));
            OnPropertyChanged(nameof(SnackMeals));

            Debug.WriteLine($"Category collections updated. Breakfast: {BreakfastMeals.Count}, Lunch: {LunchMeals.Count}, Dinner: {DinnerMeals.Count}, Snack: {SnackMeals.Count}");
        }

        private void NavigateToMealDisplay()
        {
            Debug.WriteLine($"NavigateToMealDisplay called with meal: {SelectedMeal?.Name ?? "null"}");

            if (SelectedMeal == null)
            {
                Debug.WriteLine("Warning: SelectedMeal is null");
                return;
            }

            var mealViewModel = new MealViewModel();
            mealViewModel.InitializeFromMeal(SelectedMeal);

            Debug.WriteLine($"Navigating to MealDisplayPage with meal: {SelectedMeal.Name}");
            Debug.WriteLine($"Meal details - Calories: {SelectedMeal.Calories}, Protein: {SelectedMeal.Protein}, Carbs: {SelectedMeal.Carbohydrates}, Fat: {SelectedMeal.Fat}");

            _navigationService.NavigateTo(typeof(MealDisplayPage), mealViewModel);
        }

        public void NavigateBack()
        {
            _navigationService.GoBack();
        }
    }
}
