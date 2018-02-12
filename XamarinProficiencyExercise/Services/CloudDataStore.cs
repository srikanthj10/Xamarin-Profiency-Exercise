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
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"");
                items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }
            return items;
        }

        public async Task<Item> GetItemsAsync(string sortOrder)
        {
            if (CrossConnectivity.Current.IsConnected && sortOrder == Constants.SortAscending)
            {
                var json = await client.GetStringAsync($"");
                var resultString = json;
                var jsonResult = JObject.Parse(resultString);
                jsonResult["rows"] = new JArray(jsonResult["rows"].OrderBy(obj => obj["title"]));
                var jsonSorted = JsonConvert.SerializeObject(jsonResult);
                items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(jsonSorted));
            }
            return items;
        }
    }
}
