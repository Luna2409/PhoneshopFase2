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
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Brand>
            {
                new Brand{ Id = 1, BrandName = "Huawei"},
                new Brand{ Id = 2, BrandName = "Samsung"},
                new Brand{ Id = 3, BrandName = "Apple"},
                new Brand{ Id = 4, BrandName = "Google"},
                new Brand{ Id = 5, BrandName = "Xiaomi"}
            });

            var brand = new Brand()
            {
                //Id = 6,
                BrandName = "OnePlus"
            };
            
            brandService.Create(brand);
            
            var result = brandService.GetAll().ToList().Contains(brand);
            Assert.True(result);
            
            //mockRepository.SetupAdd(Create brand)
            //var list = brandService.GetAll().ToList();
            //Assert.Equal(list[5].BrandName, brand.BrandName);
        }
    }
}
