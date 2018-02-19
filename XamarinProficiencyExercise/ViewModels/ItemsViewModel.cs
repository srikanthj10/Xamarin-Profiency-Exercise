using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinProficiencyExercise.Assets;

namespace XamarinProficiencyExercise
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Row> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ICommand SortItemsCommand { get; set; }
        public ICommand RefreshItemsCommand { get; set; }
        public String PageTitle { get; set; }

        public ItemsViewModel()
        {
            //Observable collectionf or the list view
            Items = new ObservableCollection<Row>();

            //Command for Load/refresh list view
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //Command to sort list view
            SortItemsCommand = new Command(async () => await ExecuteSortItemsCommand());        

            //Commad for refresh list view
            RefreshItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            Contract.Ensures(Contract.Result<Task>() != null);
            if (IsBusy)
                return;
            
            IsBusy = true;

            try
            {
                //Clear the observable collections before adding the fresh list
                Items.Clear();

                //Get data from server
                var items = await DataStore.GetItemsAsync(true);
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

        async Task ExecuteSortItemsCommand()
        {
            if (IsBusy)
                return;
      
            IsBusy = true;
            try
            {
                //Sort the old list
                var temp = new ObservableCollection<Row>(Items.OrderBy(x => x.title).ToList());

                //Clear the existing items and refresh the list
                Items.Clear();
                foreach (var row in temp)
                {
                    Items.Add(row);
                }
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
    