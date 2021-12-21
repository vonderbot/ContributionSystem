using ContributionSystem.ViewModels.Common;
using ContributionSystem.ViewModels.Enums;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    /// <summary>
    /// Response model with calculation result.
    /// </summary>
    public class ResponseCalculateContributionViewModel : CollectionOfItems<MonthsInfoContributionViewModelItem>
    {
        /// <summary>
        /// Calculation method.
        /// </summary>
        public CalculationMethodEnumView CalculationMethod { get; set; }
    }
}