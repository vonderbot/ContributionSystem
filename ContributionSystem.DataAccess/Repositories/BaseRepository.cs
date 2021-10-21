using ContributionSystem.DataAccess.Contexts;

namespace ContributionSystem.DataAccess.Repositories
{
    abstract public class BaseRepository
    {
        protected readonly ContributionDbContext _contributionDbContext;

        protected BaseRepository(ContributionDbContext newContributionDbContext)
        {
            _contributionDbContext = newContributionDbContext;
        }

        protected void SaveChanges()
        {
            _contributionDbContext.SaveChanges();
        }
    }
}
