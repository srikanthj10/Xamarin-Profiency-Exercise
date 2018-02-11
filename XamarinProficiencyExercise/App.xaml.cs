using System;

using Xamarin.Forms;
using XamarinProficiencyExercise.Views;

namespace XamarinProficiencyExercise
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json";

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new NavigationPage(new ListPage());
            else
                MainPage = new NavigationPage(new ListPage());
        }
    }
}
