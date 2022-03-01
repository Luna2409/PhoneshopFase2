using Moq;
using Phoneshop.Business.Services;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phoneshop.Tests.PhoneServiceTests
{
    public class Delete
    {
        private readonly IPhoneService phoneService;
        private readonly IBrandService brandService;
        private readonly Mock<IRepository<Phone>> mockRepository;
        private readonly Mock<IRepository<Brand>> mockBrandRepository;

        public Delete()
        {
            mockRepository = new Mock<IRepository<Phone>>();
            mockBrandRepository = new Mock<IRepository<Brand>>();

            brandService = new BrandService(mockBrandRepository.Object);
            phoneService = new PhoneService(brandService, mockRepository.Object);
        }

        [Fact]
        public void Should_DeletePhone()
        {
            mockBrandRepository.Setup(b => b.GetAll()).Returns(new List<Brand>
            {
                new Brand { Id = 1, Name = "Huawei" },
                new Brand { Id = 2, Name = "Samsung" },
                new Brand { Id = 3, Name = "Apple" },
                new Brand { Id = 4, Name = "Google" },
                new Brand { Id = 5, Name = "Xiaomi" }
            }.AsQueryable<Brand>);
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, BrandID = 1, Description = "Kwaliteit"},
                new Phone{ Id = 2, BrandID = 2, Description = "Pixel"},
                new Phone{ Id = 3, BrandID = 3, Description = "Pixel"},
                new Phone{ Id = 4, BrandID = 4, Description = "Kwaliteit"},
                new Phone{ Id = 5, BrandID = 5, Description = "Kwaliteit"}
            }.AsQueryable<Phone>);

            phoneService.Delete(4);

            mockRepository.Verify(x => x.Delete(4), Times.Once);
        }

        [Fact]
        public void Should_Throw_ArgumentOutOfRangeException()
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

            Assert.Throws<ArgumentOutOfRangeException>(() => phoneService.Delete(0));
        }
    }
}
