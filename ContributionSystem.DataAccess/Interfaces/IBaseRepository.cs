using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(object id);

        Task Create(T obj);

        void Update(T obj);

        Task Delete(object id);

        Task Save();
    }
}
