using ContributionSystem.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IContributionRepository : IBaseRepository<Contribution>
    {
        public Task<int> GetNumberOfUserRecords(string userId);

        public Task<List<Contribution>> Get(int take, int skip);

        public Task<List<Contribution>> GetByUserId(int take, int skip, string id);
    }
}
