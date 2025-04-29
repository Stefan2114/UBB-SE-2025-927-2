using Microsoft.UI.Xaml;
using System;

using Microsoft.Data.SqlClient;
using MealPlannerProject;
using Microsoft.UI.Xaml.Controls;
using MealPlannerProject.Services;
using MealPlannerProject.ViewModels;
using MealPlannerProject.Pages;





namespace MealPlannerProject
{
    public partial class App : Application
    {
        public Window Window { get; set; }

        public static Window MainWindow { get; private set; }
        public static MealListViewModel MealListViewModel { get; private set; }



        public App()
        {
            this.InitializeComponent();
            this.UnhandledException += OnUnhandledException;
            
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
                    rootFrame.Navigate(typeof(MealPlannerProject.Pages.WelcomePage));
                }

                // Initialize the shared ViewModel with the required MealService instance  
                var mealService = new MealService(); // Ensure MealService is properly instantiated  
                MealListViewModel = new MealListViewModel();

                Window.Activate();
            }
        }
    }
}
