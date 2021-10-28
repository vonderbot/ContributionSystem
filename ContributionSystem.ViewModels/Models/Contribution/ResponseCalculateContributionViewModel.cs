using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Items.Contribution;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseCalculateContributionViewModel : CollectionOfItems<MonthsInfoContributionViewModelItem>
    {
        public CalculationMethodEnumView CalculationMethod { get; set; }
    }
}