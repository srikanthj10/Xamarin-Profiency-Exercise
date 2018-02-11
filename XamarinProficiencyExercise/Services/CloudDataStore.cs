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

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        { // || item.Id == null
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.title}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
