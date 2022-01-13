using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phoneshop.Tests.PhoneServiceTests
{
    public class GetAll
    {
        private readonly PhoneService phoneService;
        private readonly BrandService brandService;
        private readonly Mock<IRepository<Phone>> mockRepository;
        private readonly Mock<IRepository<Brand>> mockBrandRepository;

        public GetAll()
        {
            mockRepository = new Mock<IRepository<Phone>>();
            mockBrandRepository = new Mock<IRepository<Brand>>();

            brandService = new BrandService(mockBrandRepository.Object);
            phoneService = new PhoneService(brandService, mockRepository.Object);
        }

        [Fact]
        public void Should_GiveListOfFivePhones()
        {
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = "Huawei"},
                new Phone{ Id = 2, Brand = "Samsung"},
                new Phone{ Id = 3, Brand = "Apple"},
                new Phone{ Id = 4, Brand = "Google"},
                new Phone{ Id = 5, Brand = "Xiaomi"}
            });

            var phones = phoneService.GetAll().ToList();
            var count = phones.Count;

            Assert.Equal(5, count);
        }
    }
}
