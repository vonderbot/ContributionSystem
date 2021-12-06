using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ContributionDbContext ContributionDbContext;
        protected readonly DbSet<T> Table;

        protected BaseRepository(ContributionDbContext contributionDbContext)
        {
            ContributionDbContext = contributionDbContext;
            Table = ContributionDbContext.Set<T>();
        }

        public async Task<int> GetNumberOfRecords()
        {
            return await Table.CountAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Table.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public async Task Delete(int id)
        {
            var existing = await Table.FindAsync(id);

            if (existing != null)
            {
                Table.Remove(existing);
            }
            else
            {
                throw new Exception("Unable to delete, id does not exist.");
            }
        }

        public async Task Save()
        {
            await ContributionDbContext.SaveChangesAsync();
        }
    }
}