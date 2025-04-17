using Microsoft.AspNetCore.Components;
using VintageCashCowTechTestUI.Client.Mappers;
using VintageCashCowTechTestUI.Client.Services.Product;
using VintageCashCowTechTestUI.Client.ViewModels;

namespace VintageCashCowTechTestUI.Client.Components
{
    public partial class ProductPriceHistory : ComponentBase
    {
        private ProductPriceHistoryViewModel? Model { get; set; }

        [Parameter]
        public required int ProductId { get; set; }

        [Inject]
        public required IProductService ProductService { get; set; }

        [Inject] 
        public required IProductPriceHistoryViewModelMapper ProductPriceHistoryMapper { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var product = await ProductService.GetProductAsync(ProductId);

            Model = ProductPriceHistoryMapper.Map(product);
        }
    }
}
