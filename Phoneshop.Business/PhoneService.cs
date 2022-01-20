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
            //var phones = _phoneRepository.GetAll().Include(x => x.Brand)/*.AsEnumerable()*/;

            var phones = _phoneRepository.GetAll();
            var brands = _brandService.GetAll();

            foreach (var phone in phones)
            {
                var brand = brands.FirstOrDefault(x => x.Id == phone.BrandID);

                phone.Brand = brand;
            }

            return phones;
        }

        public Phone Get(int id)
        {
            var phone = Phones().SingleOrDefault(x => x.Id == id);
            //var foundPhone = _phoneRepository.GetById(id);
            //foundPhone.Brand = phone.Brand;

            return phone;
        }

        public IEnumerable<Phone> GetAll()
        {
            //var phones = _phoneRepository.GetAll().Include(x => x.Brand);

            return Phones()
                .OrderBy(x => x.Brand.Name).ThenBy(x => x.Type);
        }

        public IEnumerable<Phone> Search(string query)
        {
            IEnumerable<Phone> phones = Phones();
            var search = phones.Where(x => x.Brand.Name.ToLower().Contains(query.ToLower()) 
                                        || x.Type.ToLower().Contains(query.ToLower()) 
                                        || x.Description.ToLower().Contains(query.ToLower()));

            return search.OrderBy(x => x.Brand.Name);
        }

        public void Delete(int id)
        {
            _phoneRepository.Delete(id);
        }

        public void Create(Phone phone)
        {
            List<Phone> phoneList = Phones().ToList();
            List<Brand> brandList = _brandService.GetAll().ToList();

            var hasMatch = phoneList.Any(x => x.FullName.ToLower() == phone.FullName.ToLower());

            if (!hasMatch)
            {
                var hasBrand = brandList.Any(x => x.Name.ToLower() == phone.Brand.Name.ToLower());

                if (!hasBrand)
                {
                    var brand = new Brand
                    {
                        Name = phone.Brand.Name
                    };

                    _brandService.Create(brand);
                }

                List<Brand> newBrandList = _brandService.GetAll().ToList();
                var brandItem = newBrandList.Find(x => x.Name.ToLower() == phone.Brand.Name.ToLower());
                phone.BrandID = brandItem.Id;

                _phoneRepository.Create(phone);
            }
        }
    }
}