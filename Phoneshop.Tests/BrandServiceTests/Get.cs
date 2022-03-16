using Moq;
using Phoneshop.Business.Services;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Phoneshop.Tests.BrandServiceTests
{
    public class Get
    {
        private readonly BrandService brandService;
        private readonly Mock<IRepository<Brand>> mockRepository;

        public Get()
        {
            mockRepository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(mockRepository.Object);
        }

        [Fact]
        public async void Should_GetBrandById()
        {
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Brand
            {
                Id = 1, 
                Name = "Huawei"
            });

            var name = await brandService.GetByIdAsync(1);
            Assert.Equal("Huawei", name.Name);
        }

        [Fact]
        public async void Should_Throw_ArgumentOutOfRangeException()
        {
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Brand
            {
                Id = 1,
                Name = "Huawei"
            });

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => brandService.GetByIdAsync(0));
        }
    }
}
