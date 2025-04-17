using Microsoft.AspNetCore.Components;
using Radzen;
using VintageCashCowTechTestUI.Client.Services;
using VintageCashCowTechTestUI.Client.Services.Product;

namespace VintageCashCowTechTestUI.Client.Components
{
    public partial class ApplyDiscountForm : ComponentBase
    {
        private string? _serverError;
        private bool _saving;

        [Parameter]
        public int ProductId { get; set; }

        [Parameter]
        public required string ProductName { get; set; }

        [Parameter]
        public required decimal ProductPrice { get; set; }

        [Parameter]
        public required EventCallback<int> OnSaveSuccessful { get; set; }

        private int DiscountPercentage { get; set; } 

        [Inject]
        public required IProductService ProductService { get; set; }

        [Inject]
        public required DialogService DialogService { get; set; }

        private async Task SaveClicked()
        {
            _saving = true;
            _serverError = null;
            try
            {
                var response = await ProductService.ApplyDiscountAsync(ProductId, DiscountPercentage);
            }
            catch (ValidationException ve)
            {
                _serverError = ve.Message;
            }
            finally
            {
                _saving = false;
            }

            if (_serverError == null)
            {
                DialogService.Close(true);
                await OnSaveSuccessful.InvokeAsync(ProductId);
            }
        }
    }
}
