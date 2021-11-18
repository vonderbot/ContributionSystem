using ContributionSystem.ViewModels.Common;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;

namespace ContributionSystem.UI.Components
{
    public partial class ContributionCalculatorTable<T, U> : ComponentBase where T : CollectionOfItems<U> where U : MonthsInfoContributionViewModelItem
    {
        [Parameter]
        public T ResponseCalculateContributionViewModel { get; set; }

        [Parameter]
        public EventCallback<T> ResponseCalculateContributionViewModelChanged { get; set; }
    }
}
