using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ContributionSystem.DataAccess.Repositories
{
    public class ContributionRepository : IContributionRepository
    {
        private readonly ContributionDbContext _contributionDbContext;

        public ContributionRepository(ContributionDbContext newContributionDbContext)
        {
            _contributionDbContext = newContributionDbContext;
        }

        //public IEnumerable<Contribution> GetContributionList()
        //{
        //    return _contributionDbContext.Contribution.ToList();
        //}

        public void Create(Contribution newContribution, IEnumerable<MonthInfo> details)
        {
            _contributionDbContext.Contribution.Add(newContribution);
            _contributionDbContext.SaveChanges();
            foreach (var element in details)
            {
                _contributionDbContext.MonthInfo.Add(element);
            }
            _contributionDbContext.SaveChanges();
        }
    }
}
