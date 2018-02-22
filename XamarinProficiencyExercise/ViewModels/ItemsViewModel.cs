using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinProficiencyExercise.Assets;

namespace XamarinProficiencyExercise.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Row> Items { get; set; }
        public List<Row> UnSortedItems { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ICommand SortItemsCommand { get; set; }
        public ICommand RefreshItemsCommand { get; set; }
        public String PageTitle { get; set; }
  
        public ItemsViewModel()
        {
            //Observable collectionf or the list view
            Items = new ObservableCollection<Row>();
            UnSortedItems = new List<Row>();         

            //Load Data from the server
            InitializeListView();

            //Command for Load/refresh list view
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //Command to sort list view
            SortItemsCommand = new Command(async () => await ExecuteSortItemsCommand());        

            //Commad for refresh list view
            RefreshItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async void InitializeListView() {
            await ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            Contract.Ensures(Contract.Result<Task>() != null);
            if (IsBusy)
                return;
            
            IsBusy = true;
            GridHeight = Constants.initlaGridHeight;

            try
            {
                //Clear the observable collections before adding the fresh list
                Items.Clear();

                //Get data from server
                var items = Task.Run( async () => await DataStore.GetItemsAsync(true)).Result;
                PageTitle = items.title.ToUpper();

                //Validate and Take all the valid datas from the response
                foreach (var row in items.rows)
                {         
                     if (row.title != null)
                     {
                        row.description = (row.description == null) ? Constants.NoDescription : row.description;
                        row.imageHref = (row.imageHref == null) ? Constants.NoImageURL : row.imageHref;
                        Items.Add(row);
                     }
                }
                GridHeight = Constants.gridHeight;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;               
                this.UnSortedItems = Items.ToList();
                IsSorted = false;
            }
        }

        async Task ExecuteSortItemsCommand()
        {
            if (IsBusy)
                return;
      
            IsBusy = true;
            GridHeight = Constants.initlaGridHeight;
            
            try
            {
                //Check if the UI has sorted items displayed
                if(!IsSorted){
                    
                    //Sort the old list model
                    var sortedItems = new ObservableCollection<Row>(Items.OrderBy(x => x.title).ToList());

                    //Clear the old list and refresh UI with the new list
                    Items.Clear();
                    foreach (var row in sortedItems)
                    {
                        Items.Add(row);
                    }
                    IsSorted = true;
                }
                else
                {
                    //Clear the old list and refresh UI with the unsorted list
                    Items.Clear();
                    foreach (var row in this.UnSortedItems)
                    {
                        Items.Add(row);
                    }
                    IsSorted = false;
                }
                GridHeight = Constants.gridHeight;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
    