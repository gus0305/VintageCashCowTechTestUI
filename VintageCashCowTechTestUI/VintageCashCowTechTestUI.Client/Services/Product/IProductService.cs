using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;

namespace VintageCashCowTechTestUI.Client.Services.Product
{
    public interface IProductService
    {
        Task<DiscountResponse?> ApplyDiscountAsync(int productId, int discountPercentage);
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<ProductPriceHistoryResponse?> GetProductAsync(int productId);
        Task<UpdatePriceResponse?> UpdatePriceAsync(int productId, decimal newPrice);
    }
}