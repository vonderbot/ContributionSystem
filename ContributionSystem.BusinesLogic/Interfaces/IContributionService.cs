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
        /// Gets months info for contributions.
        /// </summary>
        /// <param name="id">Contribution id.</param>
        /// <returns>Response model with list of months info.</returns>
        public Task<ResponseGetDetailsByIdContributionViewModel> GetDetailsById(int id);

        /// <summary>
        /// Calculates new request and adds it to db.
        /// </summary>
        /// <param name="request">Request model with info for calculation.</param>
        /// <returns>Response model with calculation result.</returns>
        public Task<ResponseCalculateContributionViewModel> Calculate(RequestCalculateContributionViewModel request);

        /// <summary>
        /// Gets user calculations history.
        /// </summary>
        /// <param name="request">Request model with info to get a piece of data.</param>
        /// <returns>Response model with list of general calculations info.</returns>
        public Task<ResponseGetHistoryByUserIdContributionViewModel> GetHistoryByUserId(RequestGetHistoryByUserIdContributionViewModel request);
    }
}