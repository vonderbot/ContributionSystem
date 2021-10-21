using ContributionSystem.DataAccess.Contexts;

namespace ContributionSystem.DataAccess.Repositories
{
    public class BaseRepository
    {
        private readonly ContributionDbContext _contributionDbContext;

        public BaseRepository(ContributionDbContext newContributionDbContext)
        {
            _contributionDbContext = newContributionDbContext;
        }

        private void SaveChanges()
        {
            _contributionDbContext.SaveChanges();
        }
    }
}
