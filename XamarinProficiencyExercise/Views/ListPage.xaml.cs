using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinProficiencyExercise.Views
{
    public partial class ListPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            viewModel.SortItemsCommand.Execute(null);
        }

    }
}
