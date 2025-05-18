namespace SocialApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using AppCommonClasses.Models;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Microsoft.Extensions.DependencyInjection;
    using SocialApp.Interfaces;

    public class GroceryViewModel : ObservableObject
    {
        private static long userId;
        private readonly IGroceryListService service;

        public static long UserId
        {
            get => userId;
            set => userId = value;
        }

        public ObservableCollection<GroceryIngredient> Ingredients { get; private set; } = new();

        public ObservableCollection<GroceryIngredient> MostFrequentIngredients { get; private set; }

        public ObservableCollection<GroceryIngredient> RecentlyUsedIngredients { get; private set; }

        public string newGroceryIngredientName;

        private ObservableCollection<SectionModel> sections;

        public ObservableCollection<SectionModel> Sections
        {
            get => sections;
            set
            {
                if (sections != value)
                {
                    sections = value;
                    OnPropertyChanged(nameof(Sections));
                }
            }
        }

        public string NewGroceryIngredientName
        {
            get => this.newGroceryIngredientName;
            set => this.SetProperty(ref this.newGroceryIngredientName, value);
        }

        public RelayCommand<GroceryIngredient> AddGroceryIngredientCommand { get; }

        public GroceryViewModel(IGroceryListService groceryListService)
        {
            UserId = App.Services.GetService<AppController>().CurrentUser.Id;

            this.service = groceryListService;

            this.AddGroceryIngredientCommand = new RelayCommand<GroceryIngredient>(this.AddGroceryIngredient);
            this.MostFrequentIngredients = new ObservableCollection<GroceryIngredient>
            {
                new GroceryIngredient { Name = "Tomatoes" },
                new GroceryIngredient { Name = "Onions" },
                new GroceryIngredient { Name = "Garlic" },
            };
            this.RecentlyUsedIngredients = new ObservableCollection<GroceryIngredient>
            {
                new GroceryIngredient { Name = "Olive Oil" },
                new GroceryIngredient { Name = "Salt" },
                new GroceryIngredient { Name = "Pepper" },
            };
            this.newGroceryIngredientName = string.Empty;
            _ = this.LoadUserGroceryList();
        }

        public async void AddGroceryIngredient(GroceryIngredient? ingredient = null)
        {
            GroceryIngredient resultIngredient = await this.service.AddIngredientToUser(userId, ingredient ?? GroceryIngredient.defaultIngredient, newGroceryIngredientName, sections);
            if (resultIngredient == GroceryIngredient.defaultIngredient)
            {
                return;
            }
            else
            {
                ingredient = resultIngredient;
            }

            ingredient.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                {
                    var ing = (GroceryIngredient)s;
                    _ = this.service.UpdateIsChecked(userId, ing.IngredientId, ing.IsChecked);
                }
            };

            this.Sections[0].Items.Add(ingredient);

            this.NewGroceryIngredientName = string.Empty;
        }

        private async Task LoadUserGroceryList()
        {
            var ingredientsFromDb = await this.service.GetIngredientsForUser(userId);

            foreach (var item in ingredientsFromDb)
            {
                item.PropertyChanged += OnIngredientPropertyChanged;
            }


            this.Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel
                {
                    Title = "My List",
                    Items = new ObservableCollection<GroceryIngredient>(ingredientsFromDb),
                },
            };

            OnPropertyChanged(nameof(Sections));
        }

        private void OnIngredientPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is GroceryIngredient ingredient && e.PropertyName == nameof(GroceryIngredient.IsChecked))
            {
                _ = this.service.UpdateIsChecked(ingredient.Id, ingredient.IngredientId, ingredient.IsChecked);
            }
        }
    }
}
