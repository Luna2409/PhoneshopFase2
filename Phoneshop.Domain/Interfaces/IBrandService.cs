using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoneshop.Domain.Interfaces
{
    public interface IBrandService
    {
        /// <summary>
        /// Get Details of a brand by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Brand> GetByIdAsync(int id);

        /// <summary>
        /// Gets a list of all the brands
        /// </summary>
        /// <returns></returns>
        IEnumerable<Brand> GetAll();

        /// <summary>
        /// Creates a new brand
        /// </summary>
        /// <param name="brand"></param>
        Task CreateAsync(Brand brand);

        /// <summary>
        /// Deletes a brand
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(int id);
    }
}
