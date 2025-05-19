namespace SocialApp.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Net.Http;
    using System.Windows.Input;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Services;
    using MealSocialServerMVC.Proxies;
    using Microsoft.Extensions.DependencyInjection;
    using SocialApp.Interfaces;
    using SocialApp.Pages;
    using SocialApp.Queries;
    using SocialApp.Services;

    public class MainViewModel : INotifyPropertyChanged
    {
        // Generic method to update properties
        private string circleText;
        private string baseGoalCalories;
        private string foodCalories;
        private string exerciseCalories;
        private string waterPercentage;
        private string waterGoal;

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

        public string CircleText { get => circleText; set => SetProperty(ref circleText, value); }

        public string BaseGoal_Calories { get => baseGoalCalories; set => SetProperty(ref baseGoalCalories, value); }

        public string Food_Calories { get => foodCalories; set => SetProperty(ref foodCalories, value); }

        public string Exercise_Calories { get => exerciseCalories; set => SetProperty(ref exerciseCalories, value); }

        public string Total_Protein { get => totalProtein; set => SetProperty(ref totalProtein, value); }

        public string Goal_Protein { get => goalProtein; set => SetProperty(ref goalProtein, value); }

        public string Remaining_Protein { get => remainingProtein; set => SetProperty(ref remainingProtein, value); }

        public string Total_Carbohydrates { get => totalCarbohydrates; set => SetProperty(ref totalCarbohydrates, value); }

        public string Goal_Carbohydrates { get => goalCarbohydrates; set => SetProperty(ref goalCarbohydrates, value); }

        public string Remaining_Carbohydrates { get => remainingCarbohydrates; set => SetProperty(ref remainingCarbohydrates, value); }

        public string Total_Fiber { get => totalFiber; set => SetProperty(ref totalFiber, value); }

        public string Goal_Fiber { get => goalFiber; set => SetProperty(ref goalFiber, value); }

        public string Remaining_Fiber { get => remainingFiber; set => SetProperty(ref remainingFiber, value); }

        public string Total_Fat { get => totalFat; set => SetProperty(ref totalFat, value); }

        public string Goal_Fat { get => goalFat; set => SetProperty(ref goalFat, value); }

        public string Remaining_Fat { get => remainingFat; set => SetProperty(ref remainingFat, value); }

        public string Total_Sugar { get => totalSugar; set => SetProperty(ref totalSugar, value); }

        public string Goal_Sugar { get => goalSugar; set => SetProperty(ref goalSugar, value); }

        public string Remaining_Sugar { get => remainingSugar; set => SetProperty(ref remainingSugar, value); }

        public string Water_Percentage { get => waterPercentage; set => SetProperty(ref waterPercentage, value); }

        public string Water_Goal { get => waterGoal; set => SetProperty(ref waterGoal, value); }

        // Define properties for R1 to R6
        private string[] rValues = new string[36];

        public string R1 { get => rValues[0]; set => SetProperty(ref rValues[0], value); }

        public string R1Calories { get => rValues[1]; set => SetProperty(ref rValues[1], value); }

        public string R1Diet { get => rValues[2]; set => SetProperty(ref rValues[2], value); }

        public string R1Level { get => rValues[3]; set => SetProperty(ref rValues[3], value); }

        public string R1Time { get => rValues[4]; set => SetProperty(ref rValues[4], value); }

        public string R1MealType { get => rValues[5]; set => SetProperty(ref rValues[5], value); }

        public string R2 { get => rValues[6]; set => SetProperty(ref rValues[6], value); }

        public string R2Calories { get => rValues[7]; set => SetProperty(ref rValues[7], value); }

        public string R2Diet { get => rValues[8]; set => SetProperty(ref rValues[8], value); }

        public string R2Level { get => rValues[9]; set => SetProperty(ref rValues[9], value); }

        public string R2Time { get => rValues[10]; set => SetProperty(ref rValues[10], value); }

        public string R2MealType { get => rValues[11]; set => SetProperty(ref rValues[11], value); }

        public string R3 { get => rValues[12]; set => SetProperty(ref rValues[12], value); }

        public string R3Calories { get => rValues[13]; set => SetProperty(ref rValues[13], value); }

        public string R3Diet { get => rValues[14]; set => SetProperty(ref rValues[14], value); }

        public string R3Level { get => rValues[15]; set => SetProperty(ref rValues[15], value); }

        public string R3Time { get => rValues[16]; set => SetProperty(ref rValues[16], value); }

        public string R3MealType { get => rValues[17]; set => SetProperty(ref rValues[17], value); }

        public string R4 { get => rValues[18]; set => SetProperty(ref rValues[18], value); }

        public string R4Calories { get => rValues[19]; set => SetProperty(ref rValues[19], value); }

        public string R4Diet { get => rValues[20]; set => SetProperty(ref rValues[20], value); }

        public string R4Level { get => rValues[21]; set => SetProperty(ref rValues[21], value); }

        public string R4Time { get => rValues[22]; set => SetProperty(ref rValues[22], value); }

        public string R4MealType { get => rValues[23]; set => SetProperty(ref rValues[23], value); }

        public string R5 { get => rValues[24]; set => SetProperty(ref rValues[24], value); }

        public string R5Calories { get => rValues[25]; set => SetProperty(ref rValues[25], value); }

        public string R5Diet { get => rValues[26]; set => SetProperty(ref rValues[26], value); }

        public string R5Level { get => rValues[27]; set => SetProperty(ref rValues[27], value); }

        public string R5Time { get => rValues[28]; set => SetProperty(ref rValues[28], value); }

        public string R5MealType { get => rValues[29]; set => SetProperty(ref rValues[29], value); }


        public string R6 { get => rValues[30]; set => SetProperty(ref rValues[30], value); }

        public string R6Calories { get => rValues[31]; set => SetProperty(ref rValues[31], value); }

        public string R6Diet { get => rValues[32]; set => SetProperty(ref rValues[32], value); }

        public string R6Level { get => rValues[33]; set => SetProperty(ref rValues[33], value); }

        public string R6Time { get => rValues[34]; set => SetProperty(ref rValues[34], value); }

        public string R6MealType { get => rValues[35]; set => SetProperty(ref rValues[35], value); }

        public ICommand RefreshCommand { get; }

        // Generic property setter method
        private void SetProperty(ref string field, string value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        // Commands -------------------------------------

        public ICommand SocialMedia { get; }

        public ICommand RefreshCircleTextCommand { get; }

        public ICommand RefreshBaseGoalTextCommand { get; }

        public ICommand RefreshFoodTextCommand { get; }

        public ICommand RefreshExerciseTextCommand { get; }


        public ICommand RefreshTotalPrTextCommand { get; }

        public ICommand RefreshGoalPrTextCommand { get; }

        public ICommand RefreshLeftPrTextCommand { get; }


        public ICommand RefreshTotalCarbTextCommand { get; }

        public ICommand RefreshGoalCarbTextCommand { get; }

        public ICommand RefreshLeftCarbTextCommand { get; }

        public ICommand RefreshTotalFibTextCommand { get; }

        public ICommand RefreshGoalFibTextCommand { get; }

        public ICommand RefreshLeftFibTextCommand { get; }


        public ICommand RefreshTotalFatTextCommand { get; }

        public ICommand RefreshGoalFatTextCommand { get; }

        public ICommand RefreshLeftFatTextCommand { get; }

        public ICommand RefreshTotalSugTextCommand { get; }

        public ICommand RefreshGoalSugTextCommand { get; }

        public ICommand RefreshLeftSugTextCommand { get; }

        public ICommand NavigateCommand { get; }

        public ICommand DisplayMeals { get; }

        public ICommand CreateMeal { get; }

        public ICommand GroceryList { get; }

        public ICommand AddBreakfast { get; }

        public ICommand AddLunch { get; }

        public ICommand AddDinner { get; }

        public ICommand AddSnack { get; }

        public ICommand AddWater300Command { get; }

        public ICommand AddWater400Command { get; }

        public ICommand AddWater500Command { get; }

        public ICommand AddWater750Command { get; }

        public ICommand RemoveWater300Command { get; }

        public ICommand RemoveWater400Command { get; }

        public ICommand RemoveWater500Command { get; }

        public ICommand RemoveWater750Command { get; }

        // ----------------------------------------

        private readonly IWaterIntakeService waterService;
        private readonly ICalorieService calorieService;
        private readonly ICalorieRepository calorieRepository;
        private readonly IMacrosService macrosService;

        public static long UserId { get; set; }

        public ICommand RefreshMealsCommand { get; }

        [Obsolete]
        public MainViewModel()
        {
            long number_userId = UserId;
            UserId = App.Services.GetService<AppController>().CurrentUser.Id;
            // Initialize WaterService
            waterService = new WaterIntakeService();
            waterService.AddUserIfNotExists(number_userId); // Ensure user exists in the water tracker table

            // Initialize CalorieService
            calorieService = new CalorieServiceProxy();

            // Initialize MacrosService
            macrosService = new MacrosServiceProxy(new HttpClient { BaseAddress = new Uri("https://localhost:7106/") });

            System.Diagnostics.Debug.WriteLine("Getting water intake...");

            // Initialize water intake from database
            WaterIntake = waterService.GetWaterIntake(number_userId).ToString();

            // Initialize water commands
            AddWater300Command = new RelayCommand(() => UpdateWaterIntake(300));
            AddWater400Command = new RelayCommand(() => UpdateWaterIntake(400));
            AddWater500Command = new RelayCommand(() => UpdateWaterIntake(500));
            AddWater750Command = new RelayCommand(() => UpdateWaterIntake(750));
            RemoveWater300Command = new RelayCommand(() => RemoveWaterIntake(300));
            RemoveWater400Command = new RelayCommand(() => RemoveWaterIntake(400));
            RemoveWater500Command = new RelayCommand(() => RemoveWaterIntake(500));
            RemoveWater750Command = new RelayCommand(() => RemoveWaterIntake(750));

            System.Diagnostics.Debug.WriteLine("Loading meals...");

            // Load last 6 meals
            LoadLastMeals(number_userId);

            System.Diagnostics.Debug.WriteLine("Getting food calories...");
            // Initialize calorie values from database
            Food_Calories = calorieService.GetFood(number_userId).ToString();

            BaseGoal_Calories = "2000";
            Exercise_Calories = "500";

            float circleTextNr = float.Parse(BaseGoal_Calories) - float.Parse(Food_Calories) + float.Parse(Exercise_Calories);
            CircleText = circleTextNr.ToString();

            System.Diagnostics.Debug.WriteLine("Getting macros...");
            // Initialize macros values from database
            Total_Protein = macrosService.GetProteinIntake(number_userId).ToString();
            Total_Carbohydrates = macrosService.GetCarbohydratesIntake(number_userId).ToString();
            Total_Fat = macrosService.GetFatIntake(number_userId).ToString();
            Total_Fiber = macrosService.GetFiberIntake(number_userId).ToString();
            Total_Sugar = macrosService.GetSugarIntake(number_userId).ToString();

            // Initialize values
            Goal_Protein = "30";
            Goal_Carbohydrates = "200";
            Goal_Fiber = "30";
            Goal_Fat = "90";
            Goal_Sugar = "50";

            Remaining_Protein = (float.Parse(Goal_Protein) - float.Parse(Total_Protein)).ToString();
            Remaining_Carbohydrates = (float.Parse(Goal_Carbohydrates) - float.Parse(Total_Carbohydrates)).ToString();
            Remaining_Fiber = (float.Parse(Goal_Fiber) - float.Parse(Total_Fiber)).ToString();
            Remaining_Fat = (float.Parse(Goal_Fat) - float.Parse(Total_Fat)).ToString();
            Remaining_Sugar = (float.Parse(Goal_Sugar) - float.Parse(Total_Sugar)).ToString();

            Water_Goal = "2000";

            DisplayMeals = new RelayCommand(GoDisplayMeals);

            CreateMeal = new RelayCommand(GoCreateMeal);

            GroceryList = new RelayCommand(GoGroceryList);

            AddBreakfast = new RelayCommand(GoAddBreakfast);

            AddLunch = new RelayCommand(GoAddLunch);

            AddDinner = new RelayCommand(GoAddDinner);

            AddSnack = new RelayCommand(GoAddSnack);

            SocialMedia = new RelayCommand(GoSocialMedia);

            // Initialize refresh command
            // int number_userId = int.Parse(_userId);
            // RefreshMealsCommand = new RelayCommand(LoadLastMeals);

        }

        [Obsolete]
        public void LoadLastMeals(long userId)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Starting to load last meals...");

                // Initialize all slots with default values first
                string defaultRecipe = "No meals", defaultCal = "0", defaultDiet = "N/A", defaultLevel = "N/A", defaultTime = "N/A", defaultMealType = "N/A";
                R1 = R2 = R3 = R4 = R5 = R6 = defaultRecipe;
                R1Calories = R2Calories = R3Calories = R4Calories = R5Calories = R6Calories = defaultCal;
                R1Diet = R2Diet = R3Diet = R4Diet = R5Diet = R6Diet = defaultDiet;
                R1Level = R2Level = R3Level = R4Level = R5Level = R6Level = defaultLevel;
                R1Time = R2Time = R3Time = R4Time = R5Time = R6Time = defaultTime;
                R1MealType = R2MealType = R3MealType = R4MealType = R5MealType = R6MealType = defaultMealType;

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserId", userId),
                };

                DataTable mealsTable = DataLink.Instance.ExecuteReader("dbo.get_last_6_unique_meals_pr", parameters);

                if (mealsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < mealsTable.Rows.Count && i < 6; i++)
                    {
                        var row = mealsTable.Rows[i];
                        string recipeName = row["RecipeName"]?.ToString() ?? "No Recipe";
                        string calories = row["Calories"]?.ToString() ?? "00";
                        string diet = row["Diet"]?.ToString() ?? "n/a";
                        string level = row["Level"]?.ToString() ?? "n/a";
                        string time = row["Time"]?.ToString() ?? "n/a";
                        string mealType = row["MealType"]?.ToString() ?? "n/a";

                        System.Diagnostics.Debug.WriteLine($"Loading meal {i + 1}: {recipeName}");

                        switch (i)
                        {
                            case 0: R1 = recipeName; R1Calories = calories; R1Diet = diet; R1Level = level; R1Time = time; R1MealType = mealType; break;
                            case 1: R2 = recipeName; R2Calories = calories; R2Diet = diet; R2Level = level; R2Time = time; R2MealType = mealType; break;
                            case 2: R3 = recipeName; R3Calories = calories; R3Diet = diet; R3Level = level; R3Time = time; R3MealType = mealType; break;
                            case 3: R4 = recipeName; R4Calories = calories; R4Diet = diet; R4Level = level; R4Time = time; R4MealType = mealType; break;
                            case 4: R5 = recipeName; R5Calories = calories; R5Diet = diet; R5Level = level; R5Time = time; R5MealType = mealType; break;
                            case 5: R6 = recipeName; R6Calories = calories; R6Diet = diet; R6Level = level; R6Time = time; R6MealType = mealType; break;
                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"Loaded {mealsTable.Rows.Count} meals successfully");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading last meals: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private void GoSocialMedia()
        {
            NavigationService.Instance.NavigateTo(typeof(HomeScreen));
        }

        private void GoDisplayMeals()
        {
            // NavigationService.Instance.NavigateTo(typeof(MealListPage));
            NavigationService.Instance.NavigateTo<MealListPage>();
        }

        private void GoCreateMeal()
        {
            NavigationService.Instance.NavigateTo(typeof(CreateMealPage));
        }

        private void GoGroceryList()
        {
            NavigationService.Instance.NavigateTo<GroceryListPage>();
        }

        private void GoAddBreakfast()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Breakfast");
        }

        private void GoAddLunch()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Lunch");
        }

        private void GoAddDinner()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Dinner");
        }

        private void GoAddSnack()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Snack");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Load the initial water intake from the database
        private string waterIntake;

        public string WaterIntake
        {
            get => waterIntake;
            set
            {
                if (waterIntake != value)
                {
                    waterIntake = value;
                    OnPropertyChanged(nameof(WaterIntake));
                }
            }
        }

        public void UpdateWaterIntake(float amount)
        {
            if (!float.TryParse(this.WaterIntake, out float currentIntake))
            {
                currentIntake = 0; // Default to zero if parsing fails
            }

            long number_userId = UserId;
            float newIntake = currentIntake + amount;
            this.waterService.UpdateWaterIntake(number_userId, newIntake);
            this.WaterIntake = newIntake.ToString();
        }

        public void RemoveWaterIntake(float amount)
        {
            if (!float.TryParse(this.WaterIntake, out float currentIntake))
            {
                currentIntake = 0; // Default to zero if parsing fails
            }

            long number_userId = UserId;
            float newIntake = Math.Max(0, currentIntake - amount); // Ensure we don't go below 0
            this.waterService.UpdateWaterIntake(number_userId, newIntake);
            this.WaterIntake = newIntake.ToString();
        }
    }
}
