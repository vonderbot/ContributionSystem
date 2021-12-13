using ContributionSystem.ViewModels.Models.Contribution;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Interfaces
{
    /// <summary>
    /// Provides methods to communicate with api.
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
        /// Gets user calculations history.
        /// </summary>
        /// <param name="take">Number of records to take</param>
        /// <param name="skip">Number of records to skip.</param>
        /// <returns>Response model with list of general calculations info.</returns>
        public Task<ResponseGetHistoryByUserIdContributionViewModel> GetHistoryByUserId(int take, int skip);

        /// <summary>
        /// Calculates new contribution.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response model with calculation result.</returns>
        public Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request);
    }
}
