using CleanArchitecture.Web;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.FunctionalTests.Api
{
    public class UserControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UserControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsNameAndToken()
        {
            var response = await _client.GetAsync("/api/users/user");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            
            dynamic jsonObject = JObject.Parse(stringResponse);
            var name = (string)jsonObject.name;
            var token = (string)jsonObject.token;

            Assert.Equal("Brahm Gupta", name);
            Assert.Equal("1234-455662-22233333-3333", token);
        }
    }
}
