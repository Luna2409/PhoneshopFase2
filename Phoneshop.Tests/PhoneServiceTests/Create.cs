using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new Brand { Id = 1, BrandName = "Huawei" },
                new Brand { Id = 2, BrandName = "Samsung" },
                new Brand { Id = 3, BrandName = "Apple" },
                new Brand { Id = 4, BrandName = "Google" },
                new Brand { Id = 5, BrandName = "Xiaomi" }
            });
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, BrandID = 1, Description = "Kwaliteit"},
                new Phone{ Id = 2, BrandID = 2, Description = "Pixel"},
                new Phone{ Id = 3, BrandID = 3, Description = "Pixel"},
                new Phone{ Id = 4, BrandID = 4, Description = "Kwaliteit"},
                new Phone{ Id = 5, BrandID = 5, Description = "Kwaliteit"}
            });

            var phone = new Phone()
            {
                Brand = new Brand { BrandName = "Apple"},
                Type = "IPhone Xs",
                Description = "Camera",
                PriceWithTax = 674,
                Stock = 12
            };
            phoneService.Create(phone);

            var result = phoneService.GetAll().ToList().Contains(phone);
            Assert.True(result);
        }
    }
}
