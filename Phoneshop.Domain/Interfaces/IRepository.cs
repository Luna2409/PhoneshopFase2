using System.Linq;
using System.Threading.Tasks;

namespace Phoneshop.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets everything from database
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets Entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task CreateAsync(T entity);

        /// <summary>
        /// Deletes a entity by id
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Save changes
        /// </summary>
        Task SaveChangesAsync();
    }
}
