using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;
using MealPlannerProject.Services;
using MealPlannerProject.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MealPlannerProject
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("MainWindow initialized successfully.");
                NavigationService.Instance.Initialize(MainFrame); 
                Debug.WriteLine("NavigationService initialized with MainFrame.");
                MainFrame.Navigate(typeof(Pages.UserPage)); 
                Debug.WriteLine("Navigated to GoalPage.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in MainWindow constructor: {ex.Message}");
                throw; 
            }
        }
    }
}
