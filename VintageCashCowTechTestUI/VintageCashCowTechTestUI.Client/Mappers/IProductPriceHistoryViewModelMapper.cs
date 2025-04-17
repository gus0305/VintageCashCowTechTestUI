using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;
using VintageCashCowTechTestUI.Client.ViewModels;

namespace VintageCashCowTechTestUI.Client.Mappers
{
    public interface IProductPriceHistoryViewModelMapper
    {
        ProductPriceHistoryViewModel? Map(ProductPriceHistoryResponse? productPriceHistoryResponse);
    }
}