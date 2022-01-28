using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Phoneshop.Business
{
    public class PhoneService : IPhoneService
    {
        private readonly IBrandService _brandService;
        private readonly IRepository<Phone> _phoneRepository;
        private readonly ILogger _logger;

        public PhoneService(IBrandService brandService, IRepository<Phone> phoneRepository, ILogger logger)
        {
            _brandService = brandService;
            _phoneRepository = phoneRepository;
            _logger = logger;
        }

        private IQueryable<Phone> Phones()
        {
            return _phoneRepository.GetAll().Include(x => x.Brand).OrderBy(x => x.Brand.Name).ThenBy(x => x.Type);
        }

        public Phone Get(int id)
        {
            if (id <= 0)
            {
                Exception ex = new ArgumentOutOfRangeException(nameof(id));

                _logger.Error(ex);
                throw ex;
            }

            var phone = Phones().SingleOrDefault(x => x.Id == id);

            return phone;
        }
        
        public IEnumerable<Phone> GetAll()
        {
            return Phones();
        }

        public IEnumerable<Phone> Search(string query)
        {
            _logger.Info($"Search in list for: {query}");

            return Phones().Where(x => x.Brand.Name.Contains(query)
                                        || x.Type.Contains(query)
                                        || x.Description.Contains(query));
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                Exception ex = new ArgumentOutOfRangeException(nameof(id));

                _logger.Error(ex);
                throw ex;
            }

            _phoneRepository.Delete(id);

            _logger.Info($"Deleted a phone with id: {id}");
        }

        public void Create(Phone phone)
        {
            List<Phone> phoneList = Phones().ToList();
            List<Brand> brandList = _brandService.GetAll().ToList();

            var phoneExists = phoneList.Any(x => x.FullName.ToLower() == phone.FullName.ToLower());
            if (phoneExists)
                return;

            var brandExists = brandList.Any(x => x.Name.ToLower() == phone.Brand.Name.ToLower());
            if (!brandExists)
            {
                var brand = new Brand
                {
                    Name = phone.Brand.Name
                };

                _brandService.Create(brand);
            }

            List<Brand> updatedBrandList = _brandService.GetAll().ToList();
            var brandItem = updatedBrandList.Find(x => x.Name.ToLower() == phone.Brand.Name.ToLower());
            phone.BrandID = brandItem.Id;
            var name = phone.FullName;
            phone.Brand = null;

            _phoneRepository.Create(phone);

            _logger.Info($"Created a new phone: {name}");
        }
    }
}