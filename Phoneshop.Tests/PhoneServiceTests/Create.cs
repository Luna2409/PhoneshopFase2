using Moq;
using Phoneshop.Business.Services;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phoneshop.Tests.PhoneServiceTests
{
    public class Create
    {
        private readonly IPhoneService phoneService;
        private readonly IBrandService brandService;
        private readonly Mock<IRepository<Phone>> mockRepository;
        private readonly Mock<IRepository<Brand>> mockBrandRepository;

        public Create()
        {
            mockRepository = new Mock<IRepository<Phone>>();
            mockBrandRepository = new Mock<IRepository<Brand>>();

            brandService = new BrandService(mockBrandRepository.Object);
            phoneService = new PhoneService(brandService, mockRepository.Object);
        }

        [Fact]
        public void Should_CreateNewPhone()
        {
            mockBrandRepository.Setup(b => b.GetAll()).Returns(new List<Brand>
            {
                new Brand { Id = 1, Name = "Huawei" },
                new Brand { Id = 2, Name = "Samsung" },
                new Brand { Id = 3, Name = "Apple" },
                new Brand { Id = 4, Name = "Google" },
                new Brand { Id = 5, Name = "Xiaomi" }
            }.AsQueryable);

            phoneService.CreateAsync(new Phone
            {
                Brand = new Brand { Name = "Apple" },
                Type = "IPhone Xs",
                Description = "Camera",
                PriceWithTax = 674,
                Stock = 12
            });

            mockRepository.Verify(x => x.CreateAsync(It.IsAny<Phone>()), Times.Once);
        }
    }
}
