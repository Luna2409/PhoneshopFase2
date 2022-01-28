using System.Linq;

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
        T GetById(int id);

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Create(T entity);

        /// <summary>
        /// Deletes a entity by id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Save changes
        /// </summary>
        void SaveChanges();
    }
}
