using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Phoneshop.Business.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly IBrandService _brandService;
        private readonly IRepository<Phone> _phoneRepository;

        public PhoneService(IBrandService brandService, IRepository<Phone> phoneRepository)
        {
            _brandService = brandService;
            _phoneRepository = phoneRepository;
        }

        private IQueryable<Phone> Phones()
        {
            return _phoneRepository.GetAll().Include(x => x.Brand).OrderBy(x => x.Brand.Name).ThenBy(x => x.Type);
        }

        public async Task<Phone> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var phone = await _phoneRepository.GetByIdAsync(id);

            if (phone.Brand == null)
                phone.Brand = await _brandService.GetByIdAsync(phone.BrandID);

            return phone;
        }
        
        public IEnumerable<Phone> GetAll()
        {
            return Phones();
        }

        public IEnumerable<Phone> Search(string query)
        {
            return Phones().Where(x => x.Brand.Name.Contains(query)
                                        || x.Type.Contains(query)
                                        || x.Description.Contains(query));
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            await _phoneRepository.DeleteAsync(id);
        }

        public async Task CreateAsync(Phone phone)
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

                await _brandService.CreateAsync(brand);
            }

            List<Brand> updatedBrandList = _brandService.GetAll().ToList();
            var brandItem = updatedBrandList.Find(x => x.Name.ToLower() == phone.Brand.Name.ToLower());
            phone.BrandID = brandItem.Id;
            phone.Brand = null;

            await _phoneRepository.CreateAsync(phone);
        }
    }
}