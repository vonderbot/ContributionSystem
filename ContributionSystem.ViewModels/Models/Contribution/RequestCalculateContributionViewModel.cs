using ContributionSystem.ViewModels.Enums;

namespace ContributionSystem.ViewModels.Models.Contribution
{
    /// <summary>
    /// Request model with information for calculation.
    /// </summary>
    public class RequestCalculateContributionViewModel
    {
        /// <summary>
        /// Calculation method.
        /// </summary>
        public CalculationMethodEnumView CalculationMethod { get; set; }

        /// <summary>
        /// Start value.
        /// </summary>
        public decimal StartValue { get; set; }

        /// <summary>
        /// Term.
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Percent.
        /// </summary>
        public decimal Percent { get; set; }

        /// <summary>
        /// Id of user, who made calculation.
        /// </summary>
        public string UserId { get; set; }
    }
}
