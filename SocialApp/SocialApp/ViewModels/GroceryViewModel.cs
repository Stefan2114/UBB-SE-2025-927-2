namespace SocialApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using AppCommonClasses.Data;
    using AppCommonClasses.Models;
    using AppCommonClasses.Repos;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SocialApp.Interfaces;
    using SocialApp.Proxies;
    using SocialApp.Services;

    public class GroceryViewModel : ObservableObject
    {
        private static long userId;
        private readonly IGroceryListService service;
        public ObservableCollection<SectionModel> sections;

        public static long UserId
        {
            get => userId;
            set => userId = value;
        }

        public ObservableCollection<GroceryIngredient> Ingredients { get; private set; } = new();

        public ObservableCollection<GroceryIngredient> MostFrequentIngredients { get; private set; }

        public ObservableCollection<GroceryIngredient> RecentlyUsedIngredients { get; private set; }

        public string newGroceryIngredientName;

        public ObservableCollection<SectionModel> Sections
        {
            get => this.sections;
            set => this.SetProperty(ref this.sections, value);
        }

        public string NewGroceryIngredientName
        {
            get => this.newGroceryIngredientName;
            set => this.SetProperty(ref this.newGroceryIngredientName, value);
        }

        public RelayCommand<GroceryIngredient> AddGroceryIngredientCommand { get; }

        public GroceryViewModel()
        {
            var options = new DbContextOptionsBuilder<SocialAppDbContext>()
    .UseSqlServer("Server=DESKTOP-S99JALT;Database=SocialApp;Trusted_Connection=True;TrustServerCertificate=True;")
    .Options;

            UserId = App.Services.GetService<AppController>().CurrentUser.Id;
            var dbContext = new SocialAppDbContext(options);
            var repo = new GroceryListRepository(dbContext);

            this.service = new GroceryListService(new GroceryListRepositoryProxy(), repo);
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
            this.Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel { Title = "My List" },
            };
            this.sections = new();
            this.newGroceryIngredientName = "";

            this.LoadUserGroceryList();
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

            this.Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel
                {
                    Title = "My List",
                    Items = new ObservableCollection<GroceryIngredient>(ingredientsFromDb),
                },
            };

            this.OnPropertyChanged(nameof(this.Sections));
        }
    }
}
