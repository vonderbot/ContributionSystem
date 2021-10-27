using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ContributionDbContext _contributionDbContext;
        protected readonly DbSet<T> table;

        protected BaseRepository(ContributionDbContext contributionDbContext)
        {
            _contributionDbContext = contributionDbContext;
            table = _contributionDbContext.Set<T>();
        }

        public async Task<int> GetNumberOfRecords()
        {
            return await table.CountAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            table.Update(entity);
        }

        public async Task Delete(int id)
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