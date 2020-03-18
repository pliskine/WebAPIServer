using Microsoft.Extensions.Configuration;
using NSubstitute;
using System;
using WebAPIServer.Services;
using Xunit;

namespace WebApiTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1_auth_OK()
        {
            var config = Substitute.For<IConfiguration>();
            config.GetValue<string>("secretKey").Returns("aaaaaBBBBBcccccEEEEEfffffGGGGG12");

            var userService = new UserService(config);
            userService.Users.Add(new User
            {
                Id = 1,
                Country = "fr",
                Locked = false,
                Role = "admin",
                Username = "toto",
                Password = "pwd"
            });

            var result = userService.Authenticate("toto", "pwd");

            Assert.NotNull(result);
            Assert.NotNull(result.Jwt);
            Assert.NotNull(result.RefreshToken);
        }
    }
}
