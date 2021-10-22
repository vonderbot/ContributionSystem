using ContributionSystem.ViewModels.Enums;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    public class RequestCalculateContributionViewModel
    {
        public CalculationMethodEnumView CalculationMethod { get; set; }

        public decimal StartValue { get; set; }

        public int Term { get; set; }

        public decimal Percent { get; set; }

        public  string Date { get; set; }
    }
}
