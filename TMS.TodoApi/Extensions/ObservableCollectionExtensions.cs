using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TMS.TodoApi.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
            {
                collection.Add(item);
            }
        }
    }
}
