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

        private IEnumerable<Phone> PhoneList()
        {
            var phones = _phoneRepository.GetAll();
            var brands = _brandService.GetAll();

            foreach (var item in phones)
            {
                var brand = brands.FirstOrDefault(x => x.Id == item.BrandID);

                item.Brand = brand.BrandName;
            }

            return phones;
        }

        public Phone Get(int id)
        {
            var phone = PhoneList().FirstOrDefault(x => x.Id == id);
            var foundPhone = _phoneRepository.GetById(id);
            foundPhone.Brand = phone.Brand;

            return foundPhone;
        }

        public IEnumerable<Phone> GetAll()
        {
            IEnumerable<Phone> phones = PhoneList();

            return phones.OrderBy(x => x.Brand);
        }

        public IEnumerable<Phone> Search(string query)
        {
            IEnumerable<Phone> phones = PhoneList();

            return phones.Where(x => x.Brand.ToLower().Contains(query.ToLower()) || x.Type.ToLower().Contains(query.ToLower()) || x.Description.ToLower().Contains(query.ToLower())).OrderBy(x => x.Brand);
        }

        public void Delete(int id)
        {
            _phoneRepository.Delete(id);
        }

        public void Create(Phone phone)
        {
            List<Phone> phoneList = PhoneList().ToList();
            List<Brand> brandList = _brandService.GetAll().ToList();

            var hasMatch = phoneList.Any(x => x.FullName.ToLower() == phone.FullName.ToLower());

            if (!hasMatch)
            {
                var hasBrand = brandList.Any(x => x.BrandName.ToLower() == phone.Brand.ToLower());

                if (!hasBrand)
                {
                    var brand = new Brand
                    {
                        BrandName = phone.Brand
                    };

                    _brandService.Create(brand);
                }

                List<Brand> newBrandList = _brandService.GetAll().ToList();
                var brandItem = newBrandList.Find(x => x.BrandName.ToLower() == phone.Brand.ToLower());
                phone.BrandID = brandItem.Id;

                _phoneRepository.Create(phone);
            }
        }
    }
}