using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System.Collections.Generic;

namespace Phoneshop.Business
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _brandRepository;

        public BrandService(IRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public void Create(Brand brand)
        {
            _brandRepository.Create(brand);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }
    }
}
