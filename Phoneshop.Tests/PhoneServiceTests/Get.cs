using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System;

namespace Phoneshop.Tests.PhoneServiceTests
{
    public class Get
    {
        private readonly PhoneService phoneService;
        private readonly Mock<IRepository<Phone>> mockPhoneRepository;
        private readonly Mock<IBrandService> mockBrandRepository;
        private readonly Mock<ILogger> mockLogger;

        public Get()
        {
            mockPhoneRepository = new Mock<IRepository<Phone>>();
            mockBrandRepository = new Mock<IBrandService>();
            mockLogger = new Mock<ILogger>();
;
            phoneService = new PhoneService(mockBrandRepository.Object, mockPhoneRepository.Object, mockLogger.Object);
        }

        [Theory]
        [InlineData(1, "Huawei")]
        [InlineData(2, "Samsung")]
        [InlineData(3, "Apple")]
        [InlineData(4, "Google")]
        [InlineData(5, "Xiaomi")]
        public void Should_GetPhoneById(int id, string brand)
        {
            mockPhoneRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = new Brand{Name = "Huawei" }, Description = "Kwaliteit"},
                new Phone{ Id = 2, Brand = new Brand{Name = "Samsung" }, Description = "Pixel"},
                new Phone{ Id = 3, Brand = new Brand{Name = "Apple" }, Description = "Pixel"},
                new Phone{ Id = 4, Brand = new Brand{Name = "Google" }, Description = "Kwaliteit"},
                new Phone{ Id = 5, Brand = new Brand{Name = "Xiaomi" }, Description = "Kwaliteit"}
            }.AsQueryable);

            var phone = phoneService.Get(id);
            Assert.Equal(brand, phone.Brand.Name);
        }

        [Fact]
        public void Should_Return_Null()
        {
            mockPhoneRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = new Brand{Name = "Huawei" }, Description = "Kwaliteit"},
                new Phone{ Id = 2, Brand = new Brand{Name = "Samsung" }, Description = "Pixel"},
                new Phone{ Id = 3, Brand = new Brand{Name = "Apple" }, Description = "Pixel"},
                new Phone{ Id = 4, Brand = new Brand{Name = "Google" }, Description = "Kwaliteit"},
                new Phone{ Id = 5, Brand = new Brand{Name = "Xiaomi" }, Description = "Kwaliteit"}
            }.AsQueryable);

            var phone = phoneService.Get(50);

            Assert.Null(phone);
        }

        [Fact]
        public void Should_Throw_ArgumentOutOfRangeException()
        {
            mockPhoneRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = new Brand{Name = "Huawei" }, Description = "Kwaliteit"},
                new Phone{ Id = 2, Brand = new Brand{Name = "Samsung" }, Description = "Pixel"},
                new Phone{ Id = 3, Brand = new Brand{Name = "Apple" }, Description = "Pixel"},
                new Phone{ Id = 4, Brand = new Brand{Name = "Google" }, Description = "Kwaliteit"},
                new Phone{ Id = 5, Brand = new Brand{Name = "Xiaomi" }, Description = "Kwaliteit"}
            }.AsQueryable);

            Assert.Throws<ArgumentOutOfRangeException>(() => phoneService.Get(0));
        }
    }
}
