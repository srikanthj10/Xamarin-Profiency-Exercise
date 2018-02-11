using System;

using Xamarin.Forms;
using XamarinProficiencyExercise.Views;

namespace XamarinProficiencyExercise
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            //Invoke the Navigation page for both iOS and Android
            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new NavigationPage(new ListPage());
            else
                MainPage = new NavigationPage(new ListPage());
        }
    }
}
