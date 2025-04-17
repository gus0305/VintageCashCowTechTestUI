using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;
using VintageCashCowTechTestUI.Client.ViewModels;

namespace VintageCashCowTechTestUI.Client.Mappers
{
    public class ProductViewModelMapper : IProductViewModelMapper
    {
        public List<ProductViewModel> Map(List<ProductResponse> productResponse)
        {
            var products = new List<ProductViewModel>();
            foreach (var apiProduct in productResponse)
            {
                products.Add(new ProductViewModel
                {
                    Id = apiProduct.Id,
                    Name = apiProduct.Name,
                    LastUpdated = apiProduct.LastUpdated,
                    Price = apiProduct.Price
                });
            }
            return products;
        }
    }
}
