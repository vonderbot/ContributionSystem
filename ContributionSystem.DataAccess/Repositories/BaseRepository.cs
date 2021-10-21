using ContributionSystem.DataAccess.Contexts;

namespace ContributionSystem.DataAccess.Repositories
{
    abstract public class BaseRepository
    {
        public readonly ContributionDbContext _contributionDbContext;

        public BaseRepository(ContributionDbContext newContributionDbContext)
        {
            _contributionDbContext = newContributionDbContext;
        }

        public void SaveChanges()
        {
            _contributionDbContext.SaveChanges();
        }
    }
}
