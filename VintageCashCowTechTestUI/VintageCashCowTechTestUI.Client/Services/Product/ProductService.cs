using System.Net.Http.Json;
using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;

namespace VintageCashCowTechTestUI.Client.Services.Product
{
    public class ProductService : IProductService
    {
        private const string ProductsApiBasePath = "api/products";

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpResponseMessageHandler _httpResponseMessageHandler;

        public ProductService(
            HttpClient httpClient, 
            IConfiguration configuration, 
            IHttpResponseMessageHandler httpResponseMessageHandler)
        {
            _httpClient = httpClient;

            _configuration = configuration;
            _httpResponseMessageHandler = httpResponseMessageHandler;
        }

        private string GetHost()
        {
            var productApiHost = _configuration["ProductApiHost"] ?? throw new Exception("ProductApiHost configuration value not set");
            return productApiHost;
        }

        private string GetBaseUri()
        {
            return $"{GetHost()}/{ProductsApiBasePath}";
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var requestUri = GetBaseUri();
            var httpResponse = await _httpClient.GetAsync(requestUri);
            var response = await _httpResponseMessageHandler.Handle<List<ProductResponse>>(httpResponse);
            return response!;
        }

        public async Task<ProductPriceHistoryResponse?> GetProductAsync(int productId)
        {
            var requestUri = $"{GetBaseUri()}/{productId}";
            var httpResponse = await _httpClient.GetAsync(requestUri);
            var response = await _httpResponseMessageHandler.Handle<ProductPriceHistoryResponse>(httpResponse);
            return response;
        }

        public async Task<DiscountResponse?> ApplyDiscountAsync(int productId, int discountPercentage)
        {
            var requestUri = $"{GetBaseUri()}/{productId}/apply-discount";
            var httpResponse = await _httpClient.PostAsJsonAsync(requestUri, new DiscountRequest { DiscountPercentage = discountPercentage });
            var response = await _httpResponseMessageHandler.Handle<DiscountResponse>(httpResponse);
            return response;
        }

        public async Task<UpdatePriceResponse?> UpdatePriceAsync(int productId, decimal newPrice)
        {
            var requestUri = $"{GetBaseUri()}/{productId}/update-price";
            var httpResponse = await _httpClient.PutAsJsonAsync(requestUri, new UpdatePriceRequest { NewPrice = newPrice });
            var response = await _httpResponseMessageHandler.Handle<UpdatePriceResponse>(httpResponse);
            return response;
        }
    }
}
