using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phoneshop.Tests.PhoneServiceTests
{
    public class Search
    {
        private readonly IPhoneService phoneService;
        private readonly IBrandService brandService;
        private readonly Mock<IRepository<Phone>> mockRepository;
        private readonly Mock<IRepository<Brand>> mockBrandRepository;

        public Search()
        {
            mockRepository = new Mock<IRepository<Phone>>();
            mockBrandRepository = new Mock<IRepository<Brand>>();

            brandService = new BrandService(mockBrandRepository.Object);
            phoneService = new PhoneService(brandService, mockRepository.Object);
        }

        [Fact]
        public void Should_ReturnListOfPhones()
        {
            mockBrandRepository.Setup(b => b.GetAll()).Returns(new List<Brand>
            {
                new Brand { Id = 1, BrandName = "Huawei" },
                new Brand { Id = 2, BrandName = "Samsung" },
                new Brand { Id = 3, BrandName = "Apple" },
                new Brand { Id = 4, BrandName = "Google" },
                new Brand { Id = 5, BrandName = "Xiaomi" }
            });
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, BrandID = 1, Type = "dasd", Description = "Kwaliteit"},
                new Phone{ Id = 2, BrandID = 2, Type = "dasd", Description = "Pixel"},
                new Phone{ Id = 3, BrandID = 3, Type = "dasd", Description = "Pixel"},
                new Phone{ Id = 4, BrandID = 4, Type = "dasd", Description = "Kwaliteit"},
                new Phone{ Id = 5, BrandID = 5, Type = "dasd", Description = "Kwaliteit"}
            });

            var phones = phoneService.Search("Kwaliteit").ToList();

            Assert.Equal(3, phones.Count);
        }
    }
}
