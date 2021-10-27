using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> GetNumberOfRecords();

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task Create(T entity);

        void Update(T entity);

        Task Delete(int id);

        Task Save();
    }
}
