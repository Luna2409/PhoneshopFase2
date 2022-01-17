using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phoneshop.Tests.BrandServiceTests
{
    public class Create
    {
        private readonly BrandService brandService;
        private readonly Mock<IRepository<Brand>> mockRepository;

        public Create()
        {
            mockRepository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(mockRepository.Object);
        }

        [Fact]
        public void Should_CreateNewBrand()
        {

            brandService.Create(new Brand
            {
                BrandName = "OnePlus"
            });
            
            mockRepository.Verify(x => x.Create(It.IsAny<Brand>()), Times.Once);
        }
    }
}
