using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using System.Collections.Generic;

namespace ContributionSystem.DataAccess.Repositories
{
    public class MonthInfoRepository : BaseRepository, IMonthInfoRepository
    {
        public MonthInfoRepository(ContributionDbContext newContributionDbContext)
           : base(newContributionDbContext)
        {
        }

        //public IEnumerable<Contribution> GetContributionList()
        //{
        //    return _contributionDbContext.Contribution.ToList();
        //}

        public void Create(IEnumerable<MonthInfo> details)
        {
            foreach (var element in details)
            {
                _contributionDbContext.MonthInfo.Add(element);
            }
            SaveChanges();
        }
    }
}
