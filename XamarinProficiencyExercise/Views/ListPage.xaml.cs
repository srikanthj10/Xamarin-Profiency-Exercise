using System;
using System.Collections.Generic;
using Plugin.Connectivity;
using Xamarin.Forms;
using XamarinProficiencyExercise.Assets;
using XamarinProficiencyExercise.ViewModels;

namespace XamarinProficiencyExercise.Views
{
    public partial class ListPage : ContentPage
    {
        //Initialize list page
        public ListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.LoadData();
        }

        private async void LoadData() {
            
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert(Constants.AlertTitle, Constants.NoConnection, Constants.AlertOk);
            }
        }
    }
}
