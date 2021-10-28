using System.Collections.Generic;

namespace ContributionSystem.ViewModels.Items.Contribution
{
    public class CollectionOfItems<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}
