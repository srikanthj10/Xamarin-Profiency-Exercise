using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using XamarinProficiencyExercise.Assets;

namespace XamarinProficiencyExercise
{
    public class CloudDataStore : IDataStore<Item>
    {
        HttpClient client;
        Item items;
        HttpResponseMessage response;

        public CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{Constants.BackendUrl}");
            items = new Item();
            response = new HttpResponseMessage();
        }

        public async Task<Item> GetItemsAsync(bool forceRefresh = false)
        {
            //Check for connectivity
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                //Get data fromn the server
                response = client.GetAsync($"").Result;
                if (response.IsSuccessStatusCode)
                {
                    //Parse the json and return
                    items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(response.Content.ReadAsStringAsync().Result));
                }
            }    
            return items;
        }
    }
}
