using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Items.Contribution;
using System.Collections.Generic;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseGetDetailsContributionViewModel
    {
        public CalculationMethodEnumView CalculationMethod { get; set; }

        public IEnumerable<MonthsInfoContributionViewModelItem> Items { get; set; }
    }
}
