using System.Collections.Generic;

namespace ContributionSystem.ViewModels.Common
{
    /// <summary>
    /// Provides items collection.
    /// </summary>
    /// <typeparam name="T">Any type for collection.</typeparam>
    public class CollectionOfItems<T>
    {
        /// <summary>
        /// Items collection.
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
