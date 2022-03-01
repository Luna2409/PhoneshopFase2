using Moq;
using Phoneshop.Business.Services;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phoneshop.Tests.PhoneServiceTests
{
    public class GetAll
    {
        private readonly PhoneService phoneService;
        private readonly Mock<IRepository<Phone>> mockPhoneRepository;
        private readonly Mock<IBrandService> mockBrandRepository;

        public GetAll()
        {
            mockPhoneRepository = new();
            mockBrandRepository = new();
            phoneService = new PhoneService(mockBrandRepository.Object, mockPhoneRepository.Object);
        }

        [Fact]
        public void Should_GiveListOfFivePhones()
        {
            mockPhoneRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = new Brand{Name = "Huawei" }, Description = "Kwaliteit"},
                new Phone{ Id = 2, Brand = new Brand{Name = "Samsung" }, Description = "Pixel"},
                new Phone{ Id = 3, Brand = new Brand{Name = "Apple" }, Description = "Pixel"},
                new Phone{ Id = 4, Brand = new Brand{Name = "Google" }, Description = "Kwaliteit"},
                new Phone{ Id = 5, Brand = new Brand{Name = "Xiaomi" }, Description = "Kwaliteit"}
            }.AsQueryable);

            var result = phoneService.GetAll().ToList();

            Assert.Equal(5, result.Count);
        }
    }
}
