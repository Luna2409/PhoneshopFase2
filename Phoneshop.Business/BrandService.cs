﻿using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System.Collections.Generic;
using System.Data.SqlClient;

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
