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

            foreach (var phone in phones)
            {
                var brand = brands.FirstOrDefault(x => x.Id == phone.BrandID);

                phone.Brand = brand;
            }

            return phones;
        }

        public Phone Get(int id)
        {
            var phone = PhoneList().SingleOrDefault(x => x.Id == id);
            //var foundPhone = _phoneRepository.GetById(id);
            //foundPhone.Brand = phone.Brand;

            return phone;
        }

        public IEnumerable<Phone> GetAll()
        {
            return PhoneList().OrderBy(x => x.Brand.BrandName);

            //return _phoneRepository.GetAll();  //.OrderBy(x => x.Brand.BrandName);
        }

        public IEnumerable<Phone> Search(string query)
        {
            //IEnumerable<Phone> phones = PhoneList();
            //return phones.Where(x => x.Brand.BrandName.ToLower().Contains(query.ToLower()) || x.Type.ToLower().Contains(query.ToLower()) || x.Description.ToLower().Contains(query.ToLower())).OrderBy(x => x.Brand);

            IEnumerable<Phone> phones = PhoneList();
            var search = phones.Where(x => x.Brand.BrandName.ToLower().Contains(query.ToLower()) || x.Type.ToLower().Contains(query.ToLower()) || x.Description.ToLower().Contains(query.ToLower()));

            return search.OrderBy(x => x.Brand.BrandName);
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
                var hasBrand = brandList.Any(x => x.BrandName.ToLower() == phone.Brand.BrandName.ToLower());

                if (!hasBrand)
                {
                    var brand = new Brand
                    {
                        BrandName = phone.Brand.BrandName
                    };

                    _brandService.Create(brand);
                }

                List<Brand> newBrandList = _brandService.GetAll().ToList();
                var brandItem = newBrandList.Find(x => x.BrandName.ToLower() == phone.Brand.BrandName.ToLower());
                phone.BrandID = brandItem.Id;

                _phoneRepository.Create(phone);
            }
        }
    }
}