using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System;
using System.Collections.Generic;
using Xunit;

namespace Phoneshop.Tests.PhoneServiceTests
{
    public class Get
    {
        private readonly IPhoneService phoneService;
        private readonly IBrandService brandService;
        private readonly Mock<IRepository<Phone>> mockRepository;
        private readonly Mock<IRepository<Brand>> mockBrandRepository;


        public Get()
        {
            mockRepository = new Mock<IRepository<Phone>>();
            mockBrandRepository = new Mock<IRepository<Brand>>();

            brandService = new BrandService(mockBrandRepository.Object);
            phoneService = new PhoneService(brandService, mockRepository.Object);
        }

        [Theory]
        [InlineData(1, "Huawei")]
        [InlineData(2, "Samsung")]
        [InlineData(3, "Apple")]
        [InlineData(4, "Google")]
        [InlineData(5, "Xiaomi")]
        public void Should_GetPhoneById(int id, string brand)
        {
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = "Huawei"},
                new Phone{ Id = 2, Brand = "Samsung"},
                new Phone{ Id = 3, Brand = "Apple"},
                new Phone{ Id = 4, Brand = "Google"},
                new Phone{ Id = 5, Brand = "Xiaomi"}
            });

            var phone = phoneService.Get(id);
            Assert.Equal(brand, phone.Brand);
        }

        [Fact]
        public void Should_Return_Null()
        {
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = "Huawei"},
                new Phone{ Id = 2, Brand = "Samsung"},
                new Phone{ Id = 3, Brand = "Apple"},
                new Phone{ Id = 4, Brand = "Google"},
                new Phone{ Id = 5, Brand = "Xiaomi"}
            });

            var phone = phoneService.Get(50);

            Assert.Null(phone);

            Assert.Throws<ArgumentNullException>(() => phoneService.Get(50));
        }
    }
}
