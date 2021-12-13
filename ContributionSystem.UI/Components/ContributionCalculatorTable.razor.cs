using ContributionSystem.ViewModels.Common;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;

namespace ContributionSystem.UI.Components
{
    /// <summary>
    /// ContributionCalculatorTable component code behind.
    /// </summary>
    /// <typeparam name="T">CollectionOfItems<U></typeparam>
    /// <typeparam name="U">Inherits MonthsInfoContributionViewModelItem</typeparam>
    public partial class ContributionCalculatorTable<T, U> : ComponentBase where T : CollectionOfItems<U> where U : MonthsInfoContributionViewModelItem
    {
        /// <summary>
        /// Response model with calculation result.
        /// </summary>
        [Parameter]
        public T ResponseCalculateContributionViewModel { get; set; }

        /// <summary>
        /// EventCallback for ResponseCalculateContributionViewModel.
        /// </summary>
        [Parameter]
        public EventCallback<T> ResponseCalculateContributionViewModelChanged { get; set; }
    }
}
