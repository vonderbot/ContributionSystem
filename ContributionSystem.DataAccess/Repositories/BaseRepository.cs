using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Create(T obj)
        {
            await table.AddAsync(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _contributionDbContext.Entry(obj).State = EntityState.Modified;
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