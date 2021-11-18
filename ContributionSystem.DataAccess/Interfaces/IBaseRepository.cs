using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<int> GetNumberOfRecords();

        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(int id);

        public Task Create(T entity);

        public void Update(T entity);

        public Task Delete(int id);

        public Task Save();
    }
}
