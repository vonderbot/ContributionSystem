using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
namespace ContributionSystem.DataAccess.Repositories
{
    public class MonthInfoRepository : BaseRepository<MonthInfo>, IMonthInfoRepository
    {
        public MonthInfoRepository(ContributionDbContext contributionDbContext)
           : base(contributionDbContext)
        {
        }
    }
}
