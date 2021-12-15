using ContributionSystem.ViewModels.Models.Contribution;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Interfaces
{
    /// <summary>
    /// Provides methods for calculating contributions, and interacting with a database of calculations.
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
        /// Calculates new request and adds it to the system.
        /// </summary>
        /// <param name="request">Request model with information for calculation.</param>
        /// <returns>Response model with calculation result.</returns>
        public Task<ResponseCalculateContributionViewModel> Calculate(RequestCalculateContributionViewModel request);

        /// <summary>
        /// Gets user calculations history.
        /// </summary>
        /// <param name="request">Request model with information to get a piece of data.</param>
        /// <returns>Response model with list of general calculations information.</returns>
        public Task<ResponseGetHistoryByUserIdContributionViewModel> GetHistoryByUserId(RequestGetHistoryByUserIdContributionViewModel request);
    }
}