using Moq;
using Phoneshop.Business.Services;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phoneshop.Tests.BrandServiceTests
{
    public class Delete
    {
        private readonly BrandService brandService;
        private readonly Mock<IRepository<Brand>> mockRepository;

        public Delete()
        {
            mockRepository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(mockRepository.Object);
        }

        [Fact]
        public async void Should_DeletePhone()
        {
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Brand>
            {
                new Brand{ Id = 1, Name = "Huawei"},
                new Brand{ Id = 2, Name = "Samsung"},
                new Brand{ Id = 3, Name = "Apple"},
                new Brand{ Id = 4, Name = "Google"},
                new Brand{ Id = 5, Name = "Xiaomi"}
            }.AsQueryable);

            await brandService.DeleteAsync(3);

            mockRepository.Verify(x => x.DeleteAsync(3), Times.Once);
        }

        [Fact]
        public async void Should_Throw_ArgumentOutOfRangeException()
        {
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Brand>
            {
                new Brand{ Id = 1, Name = "Huawei"},
                new Brand{ Id = 2, Name = "Samsung"},
                new Brand{ Id = 3, Name = "Apple"},
                new Brand{ Id = 4, Name = "Google"},
                new Brand{ Id = 5, Name = "Xiaomi"}
            }.AsQueryable);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => brandService.DeleteAsync(0));
        }

    }
}
