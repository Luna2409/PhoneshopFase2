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
    public class Get
    {
        private readonly PhoneService phoneService;
        private readonly Mock<IRepository<Phone>> mockPhoneRepository;
        private readonly Mock<IBrandService> mockBrandRepository;

        public Get()
        {
            mockPhoneRepository = new();
            mockBrandRepository = new();
            phoneService = new PhoneService(mockBrandRepository.Object, mockPhoneRepository.Object);
        }

        [Fact]
        public async void Should_GetPhoneById()
        {
            mockPhoneRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Phone
            {
                Id = 1, 
                Brand = new Brand{Name = "Huawei" }, 
                Description = "Kwaliteit"
            });

            var phone = await phoneService.GetByIdAsync(1);
            Assert.Equal("Huawei", phone.Brand.Name);
        }

        [Fact]
        public async void Should_Throw_ArgumentOutOfRangeException()
        {
            mockPhoneRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = new Brand{Name = "Huawei" }, Description = "Kwaliteit"},
                new Phone{ Id = 2, Brand = new Brand{Name = "Samsung" }, Description = "Pixel"},
                new Phone{ Id = 3, Brand = new Brand{Name = "Apple" }, Description = "Pixel"},
                new Phone{ Id = 4, Brand = new Brand{Name = "Google" }, Description = "Kwaliteit"},
                new Phone{ Id = 5, Brand = new Brand{Name = "Xiaomi" }, Description = "Kwaliteit"}
            }.AsQueryable);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => phoneService.GetByIdAsync(0));
        }
    }
}
