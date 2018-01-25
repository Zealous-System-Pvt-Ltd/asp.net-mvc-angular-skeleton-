using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Data.Contracts
{
    /// <summary>
    /// The DataRepository interface.
    /// </summary>
    /// <typeparam name="TEntity"> The Entity  </typeparam>
    /// <typeparam name="TKey"> The primary Key  </typeparam>
    public interface IDataRepository<TEntity, in TKey> where TEntity : class
    {
        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Gets all records from the entity
        /// </summary>
        /// <returns>
        /// The <see cref="ICollection{T}"/>.
        /// </returns>
        Task<IQueryable<TEntity>> RetrieveAllRecordsAsync();

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Insert(TEntity entity);

        /// <summary>
        /// Updates the entity
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Update(TEntity entity);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Delete(TKey id);
    }
}