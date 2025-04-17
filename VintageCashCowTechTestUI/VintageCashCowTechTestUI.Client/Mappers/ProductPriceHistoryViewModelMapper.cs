using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;
using VintageCashCowTechTestUI.Client.ViewModels;

namespace VintageCashCowTechTestUI.Client.Mappers
{
    public class ProductPriceHistoryViewModelMapper : IProductPriceHistoryViewModelMapper
    {
        public ProductPriceHistoryViewModel? Map(ProductPriceHistoryResponse? productPriceHistoryResponse)
        {
            if (productPriceHistoryResponse == null)
            {
                return null;
            }

            var productPriceHistoryViewModel = new ProductPriceHistoryViewModel
            {
                Id = productPriceHistoryResponse.Id,
                Name = productPriceHistoryResponse.Name,
                PriceHistory = new List<PriceHistoryViewModel>()
            };

            foreach (var priceHistory in productPriceHistoryResponse.PriceHistory)
            {
                productPriceHistoryViewModel.PriceHistory.Add(new PriceHistoryViewModel { Date = priceHistory.Date, Price = priceHistory.Price });
            }

            return productPriceHistoryViewModel;
        }
    }
}
