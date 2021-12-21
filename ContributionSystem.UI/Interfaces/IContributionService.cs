using ContributionSystem.ViewModels.Models.Contribution;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Interfaces
{
    /// <summary>
    /// Provides methods to communicate with API.
    /// </summary>
    public interface IContributionService
    {
        /// <summary>
        /// Gets months information for contributions.
        /// </summary>
        /// <param name="id">Contribution identifier.</param>
        /// <returns>Response model with list of months information.</returns>
        public Task<ResponseGetDetailsByIdContributionViewModel> GetDetailsById(int id);

        /// <summary>
        /// Gets user calculations history.
        /// </summary>
        /// <param name="take">Number of records to take</param>
        /// <param name="skip">Number of records to skip.</param>
        /// <returns>Response model with list of general calculations information.</returns>
        public Task<ResponseGetHistoryByUserIdContributionViewModel> GetHistoryByUserId(int take, int skip);

        /// <summary>
        /// Calculates new contribution.
        /// </summary>
        /// <param name="request">Request model with information for calculation.</param>
        /// <returns>Response model with calculation result.</returns>
        public Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request);
    }
}
