using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Repositories
{
    /// <inheritdoc/>
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ContributionDbContext ContributionDbContext;
        protected readonly DbSet<T> Table;

        protected BaseRepository(ContributionDbContext contributionDbContext)
        {
            ContributionDbContext = contributionDbContext;
            Table = ContributionDbContext.Set<T>();
        }

        /// <inheritdoc/>
        public async Task<int> GetNumberOfRecords()
        {
            return await Table.CountAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await Table.ToListAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<T> GetById(int id)
        {
            return await Table.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task Create(T entity)
        {
            await Table.AddAsync(entity);
        }

        /// <inheritdoc/>
        public void Update(T entity)
        {
            Table.Update(entity);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task Save()
        {
            await ContributionDbContext.SaveChangesAsync();
        }
    }
}