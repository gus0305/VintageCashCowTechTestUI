using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;
using VintageCashCowTechTestUI.Client.ViewModels;

namespace VintageCashCowTechTestUI.Client.Mappers
{
    public interface IProductViewModelMapper
    {
        List<ProductViewModel> Map(List<ProductResponse> productResponse);
    }
}