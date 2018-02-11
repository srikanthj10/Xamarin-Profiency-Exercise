using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinProficiencyExercise
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<T> GetItemsAsync(bool forceRefresh = false);
        Task<T> GetItemsAsync(string SortOrder);
    }
}
