using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContributionSystem.DataAccess.Interfaces
{
    /// <summary>
    /// Provides methods for base operations with database.
    /// </summary>
    /// <typeparam name="T">Class with identifier.</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Gets the number of records.
        /// </summary>
        /// <returns>Number of records.</returns>
        public Task<int> GetNumberOfRecords();

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>Collection of records.</returns>
        public Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Gets record by identifier.
        /// </summary>
        /// <param name="id">Record identifier.</param>
        /// <returns>Record model.</returns>
        public Task<T> GetById(int id);

        /// <summary>
        /// Creates a new record.
        /// </summary>
        /// <param name="entity">Record model.</param>
        /// <returns><see cref="Task" /> with asynchronous creation operation.</returns>
        public Task Create(T entity);

        /// <summary>
        /// Updates record.
        /// </summary>
        /// <param name="entity">Record model with some new data.</param>
        public void Update(T entity);

        /// <summary>
        /// Delete record.
        /// </summary>
        /// <param name="id">Record identifier.</param>
        /// <returns><see cref="Task" /> with asynchronous delete operation.</returns>
        public Task Delete(int id);

        /// <summary>
        /// Save all changes in database.
        /// </summary>
        /// <returns><see cref="Task" /> with asynchronous saving operation.</returns>
        public Task Save();
    }
}
