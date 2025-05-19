using System;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Repos;
using AppCommonClasses.Services;
using MealSocialServerMVC.Proxies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialApp.Interfaces;
using SocialApp.Pages;
using SocialApp.Proxies;
using SocialApp.Services;
using SocialApp.ViewModels;

namespace SocialApp
{
    public partial class App : Application
    {
        public Window Window { get; set; }

        public static Window MainWindow { get; private set; }
        public static MealListViewModel MealListViewModel { get; private set; }

        public static IServiceProvider Services { get; private set; }
        public static Window CurrentWindow { get; private set; }

        public App()
        {
            this.InitializeComponent();
            this.UnhandledException += OnUnhandledException;
            var services = new ServiceCollection();
            services.AddSingleton<AppController>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<IMealRepository, MealRepository>();
            services.AddSingleton<IIngredientRepository, IngredientRepository>();
            services.AddSingleton<IIngredientRepository, IngredientRepository>();
            services.AddSingleton<IGroceryListRepository, GroceryListRepositoryProxy>();


            services.AddHttpClient<MealServiceProxy>(); // ce plm e asta???
            services.AddSingleton<IUserService, UserServiceProxy>();
            services.AddSingleton<IPostService, PostServiceProxy>();
            services.AddSingleton<ICommentService, CommentServiceProxy>();
            services.AddSingleton<ICommentService, CommentServiceProxy>();
            services.AddSingleton<IGroceryListService, GroceryListService>();
            services.AddSingleton<IMacrosService, MacrosServiceProxy>();
            services.AddSingleton<IMealService, MealServiceProxy>();
            services.AddSingleton<IWaterIntakeService, WaterIntakeServiceProxy>();
            services.AddSingleton<ICalorieService, CalorieServiceProxy>();
            services.AddSingleton<IGroupService, GroupServiceProxy>();
            services.AddSingleton<CreateMealViewModel>();
            services.AddTransient<GroceryViewModel>();
            services.AddTransient<GroceryListPage>();
            services.AddTransient<MainPage>();
            services.AddTransient<MealListViewModel>();
            services.AddTransient<MealListPage>();
            Services = services.BuildServiceProvider();
        }

        private void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // Log the exception details
            System.Diagnostics.Debug.WriteLine($"Unhandled exception: {e.Message}");
            if (e.Exception != null)
            {
                System.Diagnostics.Debug.WriteLine($"Exception stack trace: {e.Exception.StackTrace}");
            }
            e.Handled = true; // Prevent the application from closing
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (Window == null)
            {
                Window = new MainWindow();
                MainWindow = Window;

                Frame rootFrame = Window.Content as Frame;
                if (rootFrame == null)
                {
                    rootFrame = new Frame();
                    Window.Content = rootFrame;
                }

                NavigationService.Instance.Initialize(rootFrame);

                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(SocialApp.Pages.WelcomePage));
                }

                Window.Activate();
            }
        }
    }
}
