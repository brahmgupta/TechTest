using CleanArchitecture.Core.Domain;
using CleanArchitecture.Core.Services;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.UnitTests.Services
{
    public class UserServiceTest
    {
        private readonly UserService _sut;
        public UserServiceTest()
        {
            _sut = new UserService();
        }

        [Fact]
        public async Task ShouldReturnDefaultUser()
        {
            // Arrange
            var token = CancellationToken.None;
            var expectedUser = new User();

            // Act
            var userResponse = await _sut.GetUser(token);

            // Assert
            Assert.True(userResponse.IsSuccess);
            Assert.Equal(expectedUser.Name, userResponse.Value.Name);
            Assert.Equal(expectedUser.Token, userResponse.Value.Token);
        }
    }
}
