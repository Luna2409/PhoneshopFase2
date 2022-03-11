using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoneshop.Business.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _brandRepository;

        public BrandService(IRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task CreateAsync(Brand brand)
        {
            await _brandRepository.CreateAsync(brand);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }
    }
}
