using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<bool> AddItemAsync(Item item)
        {
            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
