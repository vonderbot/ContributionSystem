using ContributionSystem.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IContributionRepository : IBaseRepository<Contribution>
    {
        public Task<List<Contribution>> Get(int take, int skip);
    }
}
