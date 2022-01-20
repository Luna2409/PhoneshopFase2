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
            mockBrandRepository.Setup(b => b.GetAll()).Returns(new List<Brand>
            {
                new Brand { Id = 1, Name = "Huawei" },
                new Brand { Id = 2, Name = "Samsung" },
                new Brand { Id = 3, Name = "Apple" },
                new Brand { Id = 4, Name = "Google" },
                new Brand { Id = 5, Name = "Xiaomi" }
            }.AsQueryable);
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, BrandID = 1, Description = "Kwaliteit"},
                new Phone{ Id = 2, BrandID = 2, Description = "Pixel"},
                new Phone{ Id = 3, BrandID = 3, Description = "Pixel"},
                new Phone{ Id = 4, BrandID = 4, Description = "Kwaliteit"},
                new Phone{ Id = 5, BrandID = 5, Description = "Kwaliteit"}
            }.AsQueryable);

            var result = phoneService.GetAll().ToList();

            Assert.Equal(5, result.Count);
        }
    }
}
