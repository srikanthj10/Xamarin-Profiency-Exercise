using System;
using System.Collections.Generic;
using Plugin.Connectivity;
using Xamarin.Forms;
using XamarinProficiencyExercise.Assets;

namespace XamarinProficiencyExercise.Views
{
    public partial class ListPage : ContentPage
    {
        ItemsViewModel _ItemsViewModel;

        //Initialize list page
        public ListPage()
        {
            InitializeComponent();

            //Do necessary data binding 
            _ItemsViewModel = new ItemsViewModel();
            BindingContext = _ItemsViewModel;
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
            else
            {
                if (_ItemsViewModel.Items.Count == 0)
                    _ItemsViewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}
