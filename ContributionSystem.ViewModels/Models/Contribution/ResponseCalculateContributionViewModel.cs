using ContributionSystem.ViewModels.Enums;
using System.Collections.Generic;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class ResponseCalculateContributionViewModel
    {
        public CalculationMethodEnumView CalculationMethod { get; set; }

        public  List<ResponseCalculateContributionViewModelItem> Items { get; set; }
    }

    public class ResponseCalculateContributionViewModelItem
    {
        public int MonthNumber { get; set; }

        public decimal Income { get; set; }

        public decimal Sum { get; set; }
    }
}
