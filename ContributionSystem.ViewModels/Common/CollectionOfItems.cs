using System.Collections.Generic;

namespace ContributionSystem.ViewModels.Common
{
    public class CollectionOfItems<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}
