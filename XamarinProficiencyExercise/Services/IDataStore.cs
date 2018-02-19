using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinProficiencyExercise
{
    public interface IDataStore<T>
    {
        Task<T> GetItemsAsync(bool forceRefresh = false);
    }
}
