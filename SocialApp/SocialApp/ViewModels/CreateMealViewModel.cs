namespace SocialApp.ViewModels
{
    using AppCommonClasses.Models;
    using CommunityToolkit.Mvvm.Input;
    using global::Windows.Storage;
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.Interfaces;
    using SocialApp.Services;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class CreateMealViewModel : ViewModelBase
    {
        private readonly IMealService mealService;
        private string mealName = string.Empty; // Initialize to avoid null
        private string cookingTime = string.Empty; // Initialize to avoid null
        private string selectedMealType = string.Empty; // Initialize to avoid null
        private string selectedCookingLevel = string.Empty; // Initialize to avoid null
        private ObservableCollection<string> directions;
        private ObservableCollection<MealIngredient> ingredients;
        private StorageFile selectedImage = null!; // Use null-forgiving operator

        // Calculated macros
        private int calories;
        private int protein;
        private int carbs;
        private int fats;
        private int fiber;
        private int sugar;

        public CreateMealViewModel()
        {
            mealService = new MealService();

            // Initialize collections
            directions = new ObservableCollection<string>();
            ingredients = new ObservableCollection<MealIngredient>();

            // Initialize commands
            GoBackCommand = new RelayCommand(OnGoBack);
            AddDirectionCommand = new RelayCommand(OnAddDirection);
            AddIngredientCommand = new RelayCommand(OnAddIngredient);
            SelectMealTypeCommand = new RelayCommand<string?>(OnSelectMealType); // Fixes SA1101
            SelectCookingLevelCommand = new RelayCommand<string?>(OnSelectCookingLevel); // Fixes SA1101
        }

        public string MealName
        {
            get => mealName; // Fixes SA1101
            set
            {
                if (mealName != value) // Fixes SA1101
                {
                    mealName = value; // Fixes SA1101
                    OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public string CookingTime
        {
            get => cookingTime; // Fixes SA1101
            set
            {
                if (cookingTime != value) // Fixes SA1101
                {
                    cookingTime = value; // Fixes SA1101
                    OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public string SelectedMealType
        {
            get => selectedMealType; // Fixes SA1101
            set
            {
                if (selectedMealType != value) // Fixes SA1101
                {
                    selectedMealType = value; // Fixes SA1101
                    OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public string SelectedCookingLevel
        {
            get => selectedCookingLevel; // Fixes SA1101
            set
            {
                if (selectedCookingLevel != value) // Fixes SA1101
                {
                    selectedCookingLevel = value; // Fixes SA1101
                    OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public StorageFile SelectedImage
        {
            get => selectedImage; // Fixes SA1101
            set
            {
                if (selectedImage != value) // Fixes SA1101
                {
                    selectedImage = value; // Fixes SA1101
                    OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public ObservableCollection<string> Directions
        {
            get => directions; // Fixes SA1101
            set
            {
                if (directions != value) // Fixes SA1101
                {
                    directions = value; // Fixes SA1101
                    OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public ObservableCollection<string> IngredientNames
        {
            get => new(ingredients.Select(i => $"{i.IngredientName}: {i.Quantity} servings")); // Fixes SA1101
        }

        public ObservableCollection<MealIngredient> Ingredients
        {
            get => ingredients; // Fixes SA1101
            set
            {
                if (ingredients != value) // Fixes SA1101
                {
                    ingredients = value; // Fixes SA1101
                    OnPropertyChanged(nameof(IngredientNames)); // Fixes SA1101
                    CalculateTotalMacros(); // Fixes SA1101
                }
            }
        }

        public ICommand GoBackCommand { get; }

        public ICommand AddDirectionCommand { get; }

        public ICommand AddIngredientCommand { get; }

        public ICommand SelectMealTypeCommand { get; }

        public ICommand SelectCookingLevelCommand { get; }


        private void CalculateTotalMacros()
        {
            var calculatedIngredients = ingredients.Select(i => i.CalculateMacros()); // Fixes SA1101

            calories = (int)calculatedIngredients.Sum(i => i.Calories); // Fixes SA1101
            protein = (int)calculatedIngredients.Sum(i => i.Protein); // Fixes SA1101
            carbs = (int)calculatedIngredients.Sum(i => i.Carbs); // Fixes SA1101
            fats = (int)calculatedIngredients.Sum(i => i.Fats); // Fixes SA1101
            fiber = (int)calculatedIngredients.Sum(i => i.Fiber); // Fixes SA1101
            sugar = (int)calculatedIngredients.Sum(i => i.Sugar); // Fixes SA1101

            OnPropertyChanged(nameof(TotalCalories)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalProtein)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalCarbs)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalFats)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalFiber)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalSugar)); // Fixes SA1101
        }

        public int TotalCalories => calories; // Fixes SA1101

        public int TotalProtein => protein; // Fixes SA1101

        public int TotalCarbs => carbs; // Fixes SA1101

        public int TotalFats => fats; // Fixes SA1101

        public int TotalFiber => fiber; // Fixes SA1101

        public int TotalSugar => sugar; // Fixes SA1101

        public async Task<bool> CreateMealAsync(Meal meal)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(selectedCookingLevel)) // Fixes SA1101
                {
                    selectedCookingLevel = "Beginner"; // Fixes SA1101
                }

                // Convert cooking time to integer
                if (!int.TryParse(cookingTime, out int cookingTimeMinutes)) // Fixes SA1101
                {
                    cookingTimeMinutes = 0;
                }

                // Set all meal properties including calculated macros
                meal.Name = mealName; // Fixes SA1101
                meal.PreparationTime = cookingTimeMinutes;
                meal.CookingLevel = selectedCookingLevel; // Fixes SA1101
                meal.Calories = calories; // Fixes SA1101
                meal.Protein = protein; // Fixes SA1101
                meal.Carbohydrates = carbs; // Fixes SA1101
                meal.Fat = fats; // Fixes SA1101
                meal.Fiber = fiber; // Fixes SA1101
                meal.Sugar = sugar; // Fixes SA1101

                // Create the meal first
                int mealId = await mealService.CreateMealAsync(meal); // Fixes SA1101
                if (mealId > 0)
                {
                    // Then add the meal-ingredient relationships
                    foreach (var ingredient in ingredients) // Fixes SA1101
                    {
                        await mealService.AddIngredientToMealAsync(mealId, ingredient.IngredientId, ingredient.Quantity); // Fixes SA1101
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating meal: {ex.Message}");
                return false;
            }
        }

        private void OnGoBack()
        {
            App.NavigationController.GoBack();
        }

        private async void OnAddDirection()
        {
            var dialog = new TextBox
            {
                PlaceholderText = "Enter direction step",
            };

            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Add Direction",
                Content = dialog,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = App.MainWindow.Content.XamlRoot,
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrWhiteSpace(dialog.Text))
                {
                    Directions.Add($"{Directions.Count + 1}. {dialog.Text}"); // Fixes SA1101
                }
            }
        }

        private async void OnAddIngredient()
        {
            var quantityBox = new TextBox { PlaceholderText = "Enter quantity" };
            var ingredientBox = new TextBox { PlaceholderText = "Enter ingredient name" };

            var panel = new StackPanel();
            panel.Children.Add(ingredientBox);
            panel.Children.Add(quantityBox);

            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Add Ingredient",
                Content = panel,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = App.MainWindow.Content.XamlRoot,
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrWhiteSpace(ingredientBox.Text) && !string.IsNullOrWhiteSpace(quantityBox.Text))
                {
                    if (float.TryParse(quantityBox.Text, out float quantity))
                    {
                        // Get ingredient from database
                        var ingredient = await mealService.RetrieveIngredientByNameAsync(ingredientBox.Text); // Fixes SA1101
                        if (ingredient != Ingredient.NoIngredient)
                        {
                            var mealIngredient = new MealIngredient
                            {
                                IngredientId = ingredient.Id,
                                IngredientName = ingredient.Name,
                                Quantity = quantity,
                                Protein = ingredient.Protein,
                                Calories = ingredient.Calories,
                                Carbs = ingredient.Carbs,
                                Fats = ingredient.Fats,
                                Fiber = ingredient.Fiber,
                                Sugar = ingredient.Sugar,
                            };

                            ingredients.Add(mealIngredient); // Fixes SA1101
                            OnPropertyChanged(nameof(IngredientNames)); // Fixes SA1101
                            CalculateTotalMacros(); // Fixes SA1101
                        }
                        else
                        {
                            // Show error that ingredient wasn't found
                            var errorDialog = new ContentDialog
                            {
                                Title = "Error",
                                Content = "Ingredient not found in database",
                                CloseButtonText = "OK",
                                XamlRoot = App.MainWindow.Content.XamlRoot,
                            };
                            await errorDialog.ShowAsync();
                        }
                    }
                }
            }
        }

        private void OnSelectMealType(string? mealType) // Fixes CS8622
        {
            SelectedMealType = mealType ?? string.Empty; // Fixes SA1101 and ensures null safety
        }

        private void OnSelectCookingLevel(string? level) // Fixes CS8622
        {
            SelectedCookingLevel = level ?? string.Empty; // Fixes SA1101 and ensures null safety
        }
    }
}
