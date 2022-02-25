using Phoneshop.Domain.Entities;
using System.Collections.Generic;

namespace Phoneshop.Domain.Interfaces
{
    public interface IBrandService
    {
        /// <summary>
        /// Gets a list of all the brands
        /// </summary>
        /// <returns></returns>
        IEnumerable<Brand> GetAll();

        /// <summary>
        /// Creates a new brand
        /// </summary>
        /// <param name="brand"></param>
        void Create(Brand brand);
    }
}
