namespace SocialApp.ViewModels
{
    using AppCommonClasses.Models;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SocialApp.Services;
    using System.Collections.ObjectModel;

    public class GroceryViewModel : ObservableObject
    {
        private static int userId;

        public static int UserId
        {
            get => userId;
            set => userId = value;
        }

        private readonly GroceryListService service = new();

        public ObservableCollection<GroceryIngredient> Ingredients { get; private set; } = new();

        public ObservableCollection<GroceryIngredient> MostFrequentIngredients { get; private set; }

        public ObservableCollection<GroceryIngredient> RecentlyUsedIngredients { get; private set; }

        private ObservableCollection<SectionModel> sections;

        private string newGroceryIngredientName;

        public ObservableCollection<SectionModel> Sections
        {
            get => sections;
            set => SetProperty(ref sections, value);
        }

        public string NewGroceryIngredientName
        {
            get => newGroceryIngredientName;
            set => SetProperty(ref newGroceryIngredientName, value);
        }

        public RelayCommand<GroceryIngredient> AddGroceryIngredientCommand { get; }

        public GroceryViewModel()
        {
            AddGroceryIngredientCommand = new RelayCommand<GroceryIngredient>(AddGroceryIngredient);
            MostFrequentIngredients = new ObservableCollection<GroceryIngredient>
            {
                new GroceryIngredient { Name = "Tomatoes" },
                new GroceryIngredient { Name = "Onions" },
                new GroceryIngredient { Name = "Garlic" },
            };
            RecentlyUsedIngredients = new ObservableCollection<GroceryIngredient>
            {
                new GroceryIngredient { Name = "Olive Oil" },
                new GroceryIngredient { Name = "Salt" },
                new GroceryIngredient { Name = "Pepper" },
            };
            Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel { Title = "My List" },
            };
            sections = new();
            newGroceryIngredientName = "";

            LoadUserGroceryList();
        }

        [System.Obsolete]
        public void AddGroceryIngredient(GroceryIngredient? ingredient = null)
        {
            GroceryIngredient resultIngredient = service.AddIngredientToUser(userId, ingredient ?? GroceryIngredient.defaultIngredient, newGroceryIngredientName, sections);
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
                    service.UpdateIsChecked(userId, ing.Id, ing.IsChecked);
                }
            };

            Sections[0].Items.Add(ingredient);

            NewGroceryIngredientName = string.Empty;
        }


        private void LoadUserGroceryList()
        {
            var ingredientsFromDb = service.GetIngredientsForUser(userId);

            Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel
                {
                    Title = "My List",
                    Items = new ObservableCollection<GroceryIngredient>(ingredientsFromDb),
                },
            };

            OnPropertyChanged(nameof(Sections));
        }
    }
}
