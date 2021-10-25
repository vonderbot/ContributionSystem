using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ContributionSystem.DataAccess.Repositories
{
    abstract public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ContributionDbContext _contributionDbContext;
        protected DbSet<T> table;

        protected BaseRepository(ContributionDbContext contributionDbContext)
        {
            _contributionDbContext = contributionDbContext;
            table = _contributionDbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Create(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _contributionDbContext.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            _contributionDbContext.SaveChanges();
        }
    }
}