using ContributionSystem.ViewModels.Items.Contribution;
using Microsoft.AspNetCore.Components;

namespace ContributionSystem.UI.Components
{
    public partial class ContributionCalculatorTable<T> : ComponentBase where T : CollectionOfItems<MonthsInfoContributionViewModelItem>
    {
        [Parameter]
        public T ResponseCalculateContributionViewModel { get; set; }

        [Parameter]
        public EventCallback<T> ResponseCalculateContributionViewModelChanged { get; set; }
    }
}
