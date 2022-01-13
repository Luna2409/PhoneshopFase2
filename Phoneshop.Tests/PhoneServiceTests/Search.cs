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
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone{ Id = 1, Brand = "Huawei", Description = "Kwaliteit"},
                new Phone{ Id = 2, Brand = "Samsung", Description = "Pixel"},
                new Phone{ Id = 3, Brand = "Apple", Description = "Pixel"},
                new Phone{ Id = 4, Brand = "Google", Description = "Kwaliteit"},
                new Phone{ Id = 5, Brand = "Xiaomi", Description = "Kwaliteit"}
            });

            var phones = phoneService.Search("Kwaliteit").ToList();

            Assert.Equal(3, phones.Count);
        }
    }
}
