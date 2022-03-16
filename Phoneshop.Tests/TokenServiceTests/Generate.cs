using Microsoft.Extensions.Configuration;
using Moq;
using Phoneshop.Business.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace Phoneshop.Tests.TokenServiceTests
{
    public class Generate
    {
        private readonly TokenService tokenService;
        private readonly Mock<IConfiguration> mockConfig;

        public Generate()
        {
            mockConfig = new Mock<IConfiguration>();
            tokenService = new TokenService(mockConfig.Object);
        }

        [Fact]
        public void Should_CreateToken()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
            var claims = new List<Claim> { new Claim("test", "") };
            mockConfig.SetupSequence(x => x.GetSection(It.IsAny<string>()).Value).Returns("00000000-0000-0000-0000-000000000000").Returns("").Returns("");

            var tokenString = tokenService.Generate(claims);
            var tokenTest = tokenString.Split('.')[0];

            Assert.Equal(token, tokenTest);
        }

        [Fact]
        public void Should_ThrowArgumentNullExeption()
        {
            List<Claim> claims = new();
            Assert.Throws<ArgumentNullException>(() => tokenService.Generate(claims));
        }
    }
}
