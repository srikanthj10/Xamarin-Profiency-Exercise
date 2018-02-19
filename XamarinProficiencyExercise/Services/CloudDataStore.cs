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

        public CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{Constants.BackendUrl}");
            items = new Item();
        }

        public async Task<Item> GetItemsAsync(bool forceRefresh = false)
        {
            //Check for connectivity
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                //Get data fromn the server
                var json = await client.GetStringAsync($"");

                //Parse the json and return
                items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }    
            return items;
        }

        public async Task<Item> GetItemsAsync(string sortOrder)
        {
            //Check for connectivity
            if (CrossConnectivity.Current.IsConnected && sortOrder == Constants.SortAscending)
            {
                //Get data fromn the server
                var json = await client.GetStringAsync($"");

                //Sort the received json
                var resultString = json;
                var jsonResult = JObject.Parse(resultString);
                jsonResult["rows"] = new JArray(jsonResult["rows"].OrderBy(obj => obj["title"]));
                var jsonSorted = JsonConvert.SerializeObject(jsonResult);

                //Parse the json and return
                items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }
            return items;
        }
    }
}
