using ContributionSystem.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Interfaces
{
    /// <summary>
    /// Provides method for work "Contribution" table.
    /// </summary>
    public interface IContributionRepository : IBaseRepository<Contribution>
    {
        /// <summary>
        /// Gets number of user records.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Number of user records</returns>
        public Task<int> GetNumberOfUserRecords(string userId);

        /// <summary>
        /// Gets records calculations with details.
        /// </summary>
        /// <param name="take">Number of records to take.</param>
        /// <param name="skip">Number of records to skip.</param>
        /// <returns>List of contributions.</returns>
        public Task<List<Contribution>> Get(int take, int skip);

        /// <summary>
        /// Gets records of user calculations with details.
        /// </summary>
        /// <param name="take">Number of records to take.</param>
        /// <param name="skip">Number of records to skip.</param>
        /// <param name="userId">User id.</param>
        /// <returns>List of contributions.</returns>
        public Task<List<Contribution>> GetByUserId(int take, int skip, string userId);
    }
}
