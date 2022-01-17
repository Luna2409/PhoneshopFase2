using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phoneshop.Tests.BrandServiceTests
{
    public class GetAll
    {
        private readonly BrandService brandService;
        private readonly Mock<IRepository<Brand>> mockRepository;

        public GetAll()
        {
            mockRepository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(mockRepository.Object);
        }

        [Fact]
        public void Should_GetAllPhones()
        {
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Brand>
            {
                new Brand{ Id = 1, BrandName = "Huawei"},
                new Brand{ Id = 2, BrandName = "Samsung"},
                new Brand{ Id = 3, BrandName = "Apple"},
                new Brand{ Id = 4, BrandName = "Google"},
                new Brand{ Id = 5, BrandName = "Xiaomi"}
            });

            var result = brandService.GetAll();
            Assert.Equal(5, result.Count());
        }
    }
}
