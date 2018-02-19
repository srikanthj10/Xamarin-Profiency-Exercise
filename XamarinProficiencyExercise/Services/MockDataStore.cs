using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinProficiencyExercise.Views;

namespace XamarinProficiencyExercise
{
    public class MockDataStore : IDataStore<Item>
    {
        Item items;

        public MockDataStore()
        {
            items = new Item();
            var mockItems = new List<Item>
            {
                //Do optionals here
            };
        }

        public async Task<Item> GetItemsAsync(bool forceRefresh = false)
        {
            //Load json from the local file
            var assembly = typeof(ListPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("XamarinProficiencyExercise.Assets.Proficiency.json");

            //Serialize the json string
            var jsonSorted = JsonConvert.SerializeObject(stream);

            //Parse the json and return
            items = await Task.Run(() => JsonConvert.DeserializeObject<Item>(jsonSorted));
            return await Task.FromResult(items);
        }
    }
}
