using ContributionSystem.ViewModels.Common;
using ContributionSystem.ViewModels.Enums;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseCalculateContributionViewModel : CollectionOfItems<MonthsInfoContributionViewModelItem>
    {
        public CalculationMethodEnumView CalculationMethod { get; set; }
    }
}