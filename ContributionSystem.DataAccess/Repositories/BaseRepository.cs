using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ContributionDbContext _contributionDbContext;
        protected DbSet<T> table;

        protected BaseRepository(ContributionDbContext contributionDbContext)
        {
            _contributionDbContext = contributionDbContext;
            table = _contributionDbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
        }

        public async Task Delete(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
        }

        public async Task Save()
        {
            await _contributionDbContext.SaveChangesAsync();
        }
    }
}