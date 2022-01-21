using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Phoneshop.Business
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

        private IEnumerable<Phone> Phones()
        {
            return _phoneRepository.GetAll().Include(x => x.Brand);
        }

        public Phone Get(int id)
        {
            var phone = Phones().SingleOrDefault(x => x.Id == id);

            return phone;
        }
        
        public IEnumerable<Phone> GetAll()
        {
            return Phones().OrderBy(x => x.Brand.Name).ThenBy(x => x.Type);
        }

        public IEnumerable<Phone> Search(string query)
        {
            return Phones().Where(x => x.Brand.Name.ToLower().Contains(query.ToLower())
                                        || x.Type.ToLower().Contains(query.ToLower())
                                        || x.Description.ToLower().Contains(query.ToLower()))
                                        .OrderBy(x => x.Brand.Name).ThenBy(x => x.Type);
        }

        public void Delete(int id)
        {
            _phoneRepository.Delete(id);
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

            _phoneRepository.Create(phone);
        }
    }
}