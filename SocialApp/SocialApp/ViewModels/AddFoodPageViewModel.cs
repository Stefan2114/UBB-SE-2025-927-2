namespace SocialApp.ViewModels
{
    using AppCommonClasses.Models;
    using global::Windows.UI;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Microsoft.UI;
    using Microsoft.UI.Xaml.Media;
    using SocialApp.Pages;
    using SocialApp.Services;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class AddFoodPageViewModel : ViewModelBase
    {
        private string searchText;
        private object selectedItem;
        private bool isSearchVisible;
        private string selectedUnit = "g";
        private string connectionString;

        public ObservableCollection<object> SearchResults { get; set; } = new ObservableCollection<object>();

        private string totalProtein;
        private string goalProtein;
        private string remainingProtein;

        private string totalCarbohydrates;
        private string goalCarbohydrates;
        private string remainingCarbohydrates;

        private string totalFiber;
        private string goalFiber;
        private string remainingFiber;

        private string totalFat;
        private string goalFat;
        private string remainingFat;

        private string totalSugar;
        private string goalSugar;
        private string remainingSugar;

        private void SetProperty(ref string field, string value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        public string TotalProtein { get => totalProtein; set => SetProperty(ref totalProtein, value); }

        public string GoalProtein { get => goalProtein; set => SetProperty(ref goalProtein, value); }

        public string RemainingProtein { get => remainingProtein; set => SetProperty(ref remainingProtein, value); }

        public string TotalCarbohydrates { get => totalCarbohydrates; set => SetProperty(ref totalCarbohydrates, value); }

        public string GoalCarbohydrates { get => goalCarbohydrates; set => SetProperty(ref goalCarbohydrates, value); }

        public string RemainingCarbohydrates { get => remainingCarbohydrates; set => SetProperty(ref remainingCarbohydrates, value); }

        public string TotalFiber { get => totalFiber; set => SetProperty(ref totalFiber, value); }

        public string GoalFiber { get => goalFiber; set => SetProperty(ref goalFiber, value); }

        public string RemainingFiber { get => remainingFiber; set => SetProperty(ref remainingFiber, value); }

        public string TotalFat { get => totalFat; set => SetProperty(ref totalFat, value); }

        public string GoalFat { get => goalFat; set => SetProperty(ref goalFat, value); }

        public string RemainingFat { get => remainingFat; set => SetProperty(ref remainingFat, value); }

        public string TotalSugar { get => totalSugar; set => SetProperty(ref totalSugar, value); }

        public string GoalSugar { get => goalSugar; set => SetProperty(ref goalSugar, value); }

        public string RemainingSugar { get => remainingSugar; set => SetProperty(ref remainingSugar, value); }

        private static int userId;

        public static int UserId { get => userId; set => userId = value; }

        private readonly MacrosService macrosService;

        // FOR BACK COMMAND <-
        public AddFoodPageViewModel()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            string? localDataSource = config["LocalDataSource"];
            string? initialCatalog = config["InitialCatalog"];
            connectionString = $"Data Source={localDataSource};Initial Catalog={initialCatalog};Integrated Security=True; TrustServerCertificate = True";
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);
            AddToMealCommand = new RelayCommand(AddToMeal, CanAddToMeal);
            FetchServingUnits();

            Console.WriteLine("AddFoodPageViewModel initialized");
            Console.WriteLine($"AddToMealCommand is null: {AddToMealCommand == null}");

            int number_userId = userId;

            // Initialize MacrosService
            macrosService = new MacrosService();
            // Initialize macros values from database
            TotalProtein = macrosService.GetProteinIntake(number_userId).ToString();
            TotalCarbohydrates = macrosService.GetCarbohydratesIntake(number_userId).ToString();
            TotalFat = macrosService.GetFatIntake(number_userId).ToString();
            TotalFiber = macrosService.GetFiberIntake(number_userId).ToString();
            TotalSugar = macrosService.GetSugarIntake(number_userId).ToString();

            // Initialize values
            GoalProtein = "30";
            GoalCarbohydrates = "200";
            GoalFiber = "30";
            GoalFat = "90";
            GoalSugar = "50";

            RemainingProtein = (float.Parse(GoalProtein) - float.Parse(TotalProtein)).ToString();
            RemainingCarbohydrates = (float.Parse(GoalCarbohydrates) - float.Parse(TotalCarbohydrates)).ToString();
            RemainingFiber = (float.Parse(GoalFiber) - float.Parse(TotalFiber)).ToString();
            RemainingFat = (float.Parse(GoalFat) - float.Parse(TotalFat)).ToString();
            RemainingSugar = (float.Parse(GoalSugar) - float.Parse(TotalSugar)).ToString();
        }

        public ICommand NextCommand { get; }

        public ICommand BackCommand { get; }

        private void GoBack()
        {
            App.NavigationController.GoBack();
        }

        private void GoNext()
        {
            App.NavigationController.NavigateTo(typeof(MainPage));
        }

        // for SEARCH BAR INGREDIENT / MEAL
        public ICommand AddToMealCommand { get; }

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
                PerformSearch();
            }
        }

        public object? SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                ((RelayCommand)AddToMealCommand)?.RaiseCanExecuteChanged();
                Console.WriteLine($"SelectedItem changed to: {selectedItem}");
            }
        }

        public bool IsSearchVisible
        {
            get => isSearchVisible;
            set
            {
                isSearchVisible = value;
                OnPropertyChanged();
            }
        }

        private void PerformSearch()
        {
            SearchResults.Clear();
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                IsSearchVisible = false;
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Search Meals first
                string mealQuery = "SELECT m_id, m_name FROM meals WHERE m_name LIKE @search + '%'";
                using (SqlCommand cmd = new SqlCommand(mealQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@search", SearchText);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SearchResults.Add(new { Id = reader["m_id"], Name = reader["m_name"], Type = "Meal" });
                        }
                    }
                }

                // If no meals found, search Ingredients
                if (SearchResults.Count == 0)
                {
                    string ingredientQuery = "SELECT i_id, i_name FROM ingredients WHERE i_name LIKE @search + '%'";
                    using (SqlCommand cmd = new SqlCommand(ingredientQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@search", SearchText);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SearchResults.Add(new { Id = reader["i_id"], Name = reader["i_name"], Type = "Ingredient" });
                            }
                        }
                    }
                }
            }

            IsSearchVisible = SearchResults.Count > 0;
        }

        private bool CanAddToMeal()
        {
            bool canAdd = SelectedItem != null && servingsCount > 0;
            Console.WriteLine($"CanAddToMeal: {canAdd}, SelectedItem: {SelectedItem != null}, ServingsCount: {servingsCount}");
            return canAdd;
        }

        private void AddToMeal()
        {
            Console.WriteLine("AddToMeal method called");

            if (SelectedItem == null)
            {
                Console.WriteLine("SelectedItem is null.");
                return;
            }

            try
            {
                int selectedId = (int)SelectedItem.GetType().GetProperty("Id")?.GetValue(SelectedItem);
                string type = (string)SelectedItem.GetType().GetProperty("Type")?.GetValue(SelectedItem);

                Console.WriteLine($"Selected Item ID: {selectedId}");
                Console.WriteLine($"Selected Item Type: {type}");
                Console.WriteLine($"Servings Count: {servingsCount}");
                Console.WriteLine($"Unit Name: {SelectedUnit}");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Database connection established.");

                        string insertQuery = @"
                                        INSERT INTO daily_meals (m_id, date_eaten, servings, unit_name, total_calories, 
                                    total_protein, total_carbohydrates, total_fat, total_fiber, total_sugar)
                                        SELECT 
                                            @mealId,  -- <-- Update parameter here
                                            GETDATE(),  
                                            @servings, 
                                            @unit_name, 
                                            m.calories * @servings AS total_calories, 
                                            m.protein * @servings AS total_protein, 
                                            m.carbohydrates * @servings AS total_carbohydrates, 
                                            m.fat * @servings AS total_fat, 
                                            m.fiber * @servings AS total_fiber, 
                                            m.sugar * @servings AS total_sugar
                                        FROM meals m
                                        WHERE m.m_id = @mealId";  // <-- Update parameter here as well

                        using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@mealId", selectedId);
                            cmd.Parameters.AddWithValue("@unit_name", SelectedUnit);
                            cmd.Parameters.AddWithValue("@servings", servingsCount);

                            Console.WriteLine("Executing SQL command...");
                            int result = cmd.ExecuteNonQuery();
                            Console.WriteLine($"Rows affected: {result}");

                            if (result > 0)
                            {
                                Console.WriteLine("Food successfully added to meal!");
                            }
                            else
                            {
                                Console.WriteLine("No rows were affected. The meal might not exist in the database.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error inserting data: " + ex.Message);
                        Console.WriteLine("Stack trace: " + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing selected item: " + ex.Message);
                Console.WriteLine("Stack trace: " + ex.StackTrace);
            }

            // Reset selection
            SelectedItem = null;
            SearchText = string.Empty;
            IsSearchVisible = false;
        }





        // FOR SELECTED MEAL TYPE


        private SolidColorBrush breakfastColor = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush lunchColor = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush dinnerColor = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush snackColor = new SolidColorBrush(Colors.Transparent);

        public SolidColorBrush BreakfastColor
        {
            get => breakfastColor;
            set
            {
                breakfastColor = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush LunchColor
        {
            get => lunchColor;
            set
            {
                lunchColor = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush DinnerColor
        {
            get => dinnerColor;
            set
            {
                dinnerColor = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush SnackColor
        {
            get => snackColor;
            set
            {
                snackColor = value;
                OnPropertyChanged();
            }
        }

        public void SetCategoryHighlight(string category)
        {
            // Default colors
            BreakfastColor = new SolidColorBrush(Color.FromArgb(255, 199, 110, 78)); // Reddish brown
            LunchColor = new SolidColorBrush(Color.FromArgb(255, 91, 119, 105)); // Greenish grey
            DinnerColor = new SolidColorBrush(Color.FromArgb(255, 181, 136, 94)); // Light brown
            SnackColor = new SolidColorBrush(Color.FromArgb(255, 19, 50, 36)); // Dark green

            // Highlight selected category
            SolidColorBrush highlightColor = new SolidColorBrush(Color.FromArgb(255, 227, 199, 177)); // Beige



            switch (category)
            {
                case "Breakfast": BreakfastColor = highlightColor; break;
                case "Lunch": LunchColor = highlightColor; break;
                case "Dinner": DinnerColor = highlightColor; break;
                case "Snack": SnackColor = highlightColor; break;
            }
        }


        // SERVING UNITS

        public ObservableCollection<ServingUnitModel> ServingUnits { get; set; } = new ObservableCollection<ServingUnitModel>();

        private void FetchServingUnits()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT unit_name FROM serving_units";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var servingUnits = new ObservableCollection<ServingUnitModel>();
                        while (reader.Read())
                        {
                            servingUnits.Add(new ServingUnitModel
                            {
                                UnitName = reader["unit_name"].ToString(),
                            });
                        }

                        ServingUnits = servingUnits;
                    }
                }
            }
        }

        public string SelectedUnit
        {
            get => selectedUnit;
            set
            {
                if (selectedUnit != value)
                {
                    selectedUnit = value;
                    OnPropertyChanged(nameof(SelectedUnit));
                }
            }
        }

        private int servingsCount = 0;

        public string ServingsCount
        {
            get => $"{servingsCount} servings";
            set
            {
                // Extract just the number from the string if it contains "servings"
                string numericValue = value.Replace("servings", "").Trim();
                if (int.TryParse(numericValue, out int newValue))
                {
                    servingsCount = newValue;
                    OnPropertyChanged();
                    ((RelayCommand)AddToMealCommand)?.RaiseCanExecuteChanged();
                    Console.WriteLine($"ServingsCount updated to: {servingsCount}");
                }
                else
                {
                    Console.WriteLine($"Failed to parse servings value: '{value}'");
                }
            }
        }
    }
}
