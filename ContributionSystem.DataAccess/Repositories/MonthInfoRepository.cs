using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using System.Collections.Generic;

namespace ContributionSystem.DataAccess.Repositories
{
    public class MonthInfoRepository : BaseRepository, IMonthInfoRepository
    {
        public MonthInfoRepository(ContributionDbContext contributionDbContext)
           : base(contributionDbContext)
        {
        }

        public void Create(IEnumerable<MonthInfo> monthsInfo)
        {
            foreach (var element in monthsInfo)
            {
                _contributionDbContext.MonthInfo.Add(element);
            }
            SaveChanges();
        }
    }
}
