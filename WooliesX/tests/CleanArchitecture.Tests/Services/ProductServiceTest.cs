using CleanArchitecture.Core.Services;
using System.Threading;
using Xunit;
using NSubstitute;
using Microsoft.Extensions.Options;
using CleanArchitecture.SharedKernel.AppSettings;
using System.Threading.Tasks;
using CleanArchitecture.Core.Enum;
using System.Linq;
using RichardSzalay.MockHttp;

namespace CleanArchitecture.UnitTests.Services
{
    public class ProductServiceTest
    {
        private ProductService _sut;
        private readonly IOptions<ProductAPISettings> _productAPISettings;
        protected MockHttpMessageHandler MockHttp { get; }

        public ProductServiceTest()
        {
            _productAPISettings = Options.Create(new ProductAPISettings() { BaseUrl = "http://base", Token = "123" });
        }

        [Fact]
        public async Task ShouldReturnProductsSortedByPrice()
        {
            // Arrange
            var token = CancellationToken.None;
            var responseJSON = "[{'name':'Test Product A','price':99999,'quantity':0.0},{'name':'Test Product B','price':100,'quantity':0.0}]";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(_productAPISettings.Value.ProductsUri)
                    .Respond("application/json", responseJSON);

            var client = mockHttp.ToHttpClient();
            _sut = new ProductService(client, _productAPISettings);           

            // Act
            var productsResponse = await _sut.GetProducts(SortEnum.Low, token);
            var products = productsResponse.Value;

            // Assert
            Assert.True(productsResponse.IsSuccess);
            Assert.Equal(2, products.Count());
            Assert.Equal(100, products.FirstOrDefault().Price);
        }

        [Fact]
        public async Task ShouldReturnRecommendedProducts()
        {
            // Arrange
            var token = CancellationToken.None;
            var responseJSON = "[{'customerId':123,'products':[{'name':'Test Product A','price':99.99,'quantity':3.0},{'name':'Test Product B','price':101.99,'quantity':1.0},{'name':'Test Product F','price':999999999999.0,'quantity':1.0}]},{'customerId':23,'products':[{'name':'Test Product A','price':99.99,'quantity':2.0},{'name':'Test Product B','price':101.99,'quantity':3.0},{'name':'Test Product F','price':999999999999.0,'quantity':1.0}]},{'customerId':23,'products':[{'name':'Test Product C','price':10.99,'quantity':2.0},{'name':'Test Product F','price':999999999999.0,'quantity':2.0}]},{'customerId':23,'products':[{'name':'Test Product A','price':99.99,'quantity':1.0},{'name':'Test Product B','price':101.99,'quantity':1.0},{'name':'Test Product C','price':10.99,'quantity':1.0}]}]";

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(_productAPISettings.Value.ShopperHistoryUri )
                    .Respond("application/json", responseJSON);

            var client = mockHttp.ToHttpClient();
            _sut = new ProductService(client, _productAPISettings);

            // Act
            var productsResponse = await _sut.GetProducts(SortEnum.Recommended, token);
            var products = productsResponse.Value;

            // Assert
            Assert.True(productsResponse.IsSuccess);
            Assert.Equal(4, products.Count());
            Assert.Equal("Test Product A", products.FirstOrDefault().Name);
        }
    }
}
