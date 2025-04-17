using Microsoft.Extensions.Configuration;
using Moq.Protected;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;
using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;
using VintageCashCowTechTestUI.Client.Services.Product;
using VintageCashCowTechTestUI.Client.Services;

namespace VintageCashCowTechTestUI.Client.Tests.Unit.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private const string ProductApiHost = "https://api.example.com";
        private const string ProductsApiBasePath = "api/products";

        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IHttpResponseMessageHandler> _httpResponseMessageHandlerMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            { 
                BaseAddress = new Uri(ProductApiHost) 
            };
            _configurationMock = new Mock<IConfiguration>();
            _httpResponseMessageHandlerMock = new Mock<IHttpResponseMessageHandler>();
            
            _configurationMock.Setup(c => c["ProductApiHost"]).Returns(ProductApiHost);

            _productService = new ProductService(_httpClient, _configurationMock.Object, _httpResponseMessageHandlerMock.Object);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_WhenSuccessSuccessful_ReturnsProductList()
        {
            // Arrange
            var products = new List<ProductResponse> { new ProductResponse { Id = 1, Name = "Product1" } };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(products), Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            _httpResponseMessageHandlerMock
                .Setup(h => h.Handle<List<ProductResponse>>(responseMessage))
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.AreEqual(products, result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Product1", result[0].Name);
            _httpMessageHandlerMock
                .Protected()
                .Verify("SendAsync", Times.Once(), ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && 
                    req.RequestUri.ToString() == $"{ProductApiHost}/{ProductsApiBasePath}"),
                    ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task GetProductAsync_WhenSuccessSuccessful_ReturnsProductPriceHistory()
        {
            // Arrange
            var productId = 1;

            var productResponse = new ProductPriceHistoryResponse { Id = productId, Name = "Product1" };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(productResponse), Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            _httpResponseMessageHandlerMock
                .Setup(h => h.Handle<ProductPriceHistoryResponse>(responseMessage))
                .ReturnsAsync(productResponse);

            // Act
            var result = await _productService.GetProductAsync(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(productResponse, result);
            Assert.AreEqual(productId, result.Id);
            Assert.AreEqual("Product1", result.Name);
            _httpMessageHandlerMock
                .Protected()
                .Verify("SendAsync", Times.Once(), ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && 
                    req.RequestUri.ToString() == $"{ProductApiHost}/{ProductsApiBasePath}/{productId}"),
                    ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task ApplyDiscountAsync_WhenSuccessSuccessful_ReturnsDiscountResponse()
        {
            // Arrange
            var productId = 1;
            var discountPercentage = 10;

            var discountResponse = new DiscountResponse { Id = productId, DiscountedPrice = 12.34m };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(discountResponse), Encoding.UTF8, "application/json")
            };
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            _httpResponseMessageHandlerMock
                .Setup(h => h.Handle<DiscountResponse>(responseMessage))
                .ReturnsAsync(discountResponse);

            // Act
            var result = await _productService.ApplyDiscountAsync(productId, discountPercentage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(discountResponse, result);
            _httpMessageHandlerMock
                .Protected()
                .Verify("SendAsync", Times.Once(), ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri.ToString() == $"{ProductApiHost}/{ProductsApiBasePath}/{productId}/apply-discount" &&
                    req.Content.ReadAsStringAsync().Result.Contains(discountPercentage.ToString())
                ), ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task UpdatePriceAsync_WhenSuccessSuccessful_ReturnsUpdatePriceResponse()
        {
            // Arrange
            var productId = 1;
            var newPrice = 99.99m;

            var updatePriceResponse = new UpdatePriceResponse { Id = productId, NewPrice = newPrice };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(updatePriceResponse), Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            _httpResponseMessageHandlerMock
                .Setup(h => h.Handle<UpdatePriceResponse>(responseMessage))
                .ReturnsAsync(updatePriceResponse);

            // Act
            var result = await _productService.UpdatePriceAsync(productId, newPrice);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatePriceResponse, result);
            Assert.AreEqual(newPrice, result.NewPrice);
            _httpMessageHandlerMock
                .Protected()
                .Verify("SendAsync", Times.Once(), ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put &&
                    req.RequestUri.ToString() == $"{ProductApiHost}/{ProductsApiBasePath}/{productId}/update-price" &&
                    req.Content.ReadAsStringAsync().Result.Contains(newPrice.ToString())
                ), ItExpr.IsAny<CancellationToken>());
        }
    }
}