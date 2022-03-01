using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Phoneshop.Business.Services;

namespace Phoneshop.Tests.PhoneServiceTests
{
    public class Search
    {
        private readonly PhoneService phoneService;
        private readonly Mock<IRepository<Phone>> mockPhoneRepository;
        private readonly Mock<IBrandService> mockBrandRepository;

        public Search()
        {
            mockPhoneRepository = new();
            mockBrandRepository = new();
            phoneService = new PhoneService(mockBrandRepository.Object, mockPhoneRepository.Object);
        }

        [Fact]
        public void Should_ReturnListOfPhones()
        {
            mockPhoneRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = new Brand{Name = "Huawei" }, Type = "dasd", Description = "Kwaliteit"},
                new Phone{ Id = 2, Brand = new Brand{Name = "Samsung" }, Type = "dasd", Description = "Pixel"},
                new Phone{ Id = 3, Brand = new Brand{Name = "Apple" }, Type = "dasd", Description = "Pixel"},
                new Phone{ Id = 4, Brand = new Brand{Name = "Google" }, Type = "dasd", Description = "Kwaliteit"},
                new Phone{ Id = 5, Brand = new Brand{Name = "Xiaomi" }, Type = "dasd", Description = "Kwaliteit"}
            }.AsQueryable);

            var phones = phoneService.Search("Kwaliteit").ToList();

            Assert.Equal(3, phones.Count);
        }
    }
}
