using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinProficiencyExercise.Assets;

namespace XamarinProficiencyExercise
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command SortItemsCommand { get; set; }
 
        public ItemsViewModel()
        {
            //Observable collectionf or the list view
            Items = new ObservableCollection<Item>();

            //Command for Load/refresh list view
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //Command to sort list view
            SortItemsCommand = new Command(async () => await ExecuteSortItemsCommand());                
        }

        async Task ExecuteLoadItemsCommand()
        {          
            if (IsBusy)
                return;
            
            IsBusy = true;

            try
            {
                //Clear the observable collections before adding the fresh list
                Items.Clear();

                //Get data from server
                var items = await DataStore.GetItemsAsync(true);

                //Validate and Take all the valid datas from the response
                foreach (var row in items.rows)
                {
                    var IndividualItem = new Item();
                    if (row.title != null)
                    {
                        IndividualItem.description = (row.description == null) ? Constants.NoDescription : row.description;
                        IndividualItem.title = row.title.ToUpper();
                        IndividualItem.imageHref = (row.imageHref == null)? Constants.NoImageURL : row.imageHref;                       

                        //Add to the observable collections
                        Items.Add(IndividualItem);
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
                //Clear the observable collections before adding the fresh list
                Items.Clear();

                //Get data from server in sorted order
                var items = await DataStore.GetItemsAsync(SortOrder: Constants.SortAscending );

                //Validate and Take all the valid datas from the response
                foreach (var row in items.rows)
                {
                    var IndividualItem = new Item();
                    if (row.title != null)
                    {
                        IndividualItem.description = (row.description == null) ? Constants.NoDescription : row.description;
                        IndividualItem.title = row.title.ToUpper();
                        IndividualItem.imageHref = (row.imageHref == null) ? Constants.NoImageURL : row.imageHref;                     

                        //Add to the observable collections
                        Items.Add(IndividualItem);
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
    }
}
    