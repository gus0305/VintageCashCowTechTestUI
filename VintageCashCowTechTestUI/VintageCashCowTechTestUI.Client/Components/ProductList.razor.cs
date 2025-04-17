using Microsoft.AspNetCore.Components;
using Radzen;
using VintageCashCowTechTestUI.Client.Mappers;
using VintageCashCowTechTestUI.Client.Services.Product;
using VintageCashCowTechTestUI.Client.ViewModels;

namespace VintageCashCowTechTestUI.Client.Components
{
    public partial class ProductList : ComponentBase
    {
        private const int PriceHistoryMenuItemValue = 1;
        private const int UpdatePriceMenuItemValue = 2;
        private const int ApplyDiscountMenuItemValue = 3;

        private IList<ProductViewModel> _selectedProducts = new List<ProductViewModel>();
     
        public List<ProductViewModel>? Model { get; set; }

        [Inject]
        public required IProductService ProductService { get; set; }

        [Inject]
        public required IProductViewModelMapper ProductViewModelMapper { get; set; }    
        
        [Inject]
        public required ContextMenuService ContextMenuService { get; set;}

        [Inject]
        public required DialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Initialize(null);
        }

        private ProductViewModel? GetProduct(int? productId)
        {
            if (productId != null)
            {
                return Model?.FirstOrDefault(x => x.Id == productId);

            }
            return Model?.FirstOrDefault();
        }

        private async Task Initialize(int? productId)
        {
            var products = await ProductService.GetAllProductsAsync();

            Model = ProductViewModelMapper.Map(products);

            var selectedProduct = GetProduct(productId);
            _selectedProducts = new List<ProductViewModel>();
            if (selectedProduct != null)
            {
                _selectedProducts.Add(selectedProduct);
            }
        }

        private ContextMenuItem CreateContextMenuItem(string text, int value)
        {
            return new ContextMenuItem { Text = text, Value = value };
        }

        private void OnCellContextMenu(DataGridCellMouseEventArgs<ProductViewModel> args)
        {
            _selectedProducts = new List<ProductViewModel>
            {
                args.Data 
            };

            ContextMenuService.Open(args,
                new List<ContextMenuItem> 
                {
                    CreateContextMenuItem("Price History",PriceHistoryMenuItemValue),
                    CreateContextMenuItem("Update Price", UpdatePriceMenuItemValue),
                    CreateContextMenuItem("Apply Discount", ApplyDiscountMenuItemValue)
                },
                OnMenuItemClick
             );
        }

        private void OnMenuItemClick(MenuItemEventArgs args)
        {
            var product = _selectedProducts.FirstOrDefault();
            if (product == null)
            {
                return;
            }

            switch (args.Value)
            {
                case PriceHistoryMenuItemValue:
                    Console.WriteLine("Price History menu item clicked");
                    HandlePriceHistoryMenuItemClick(product);
                    break;

                case UpdatePriceMenuItemValue:
                    Console.WriteLine("Update Price menu item clicked");
                    HandleUpdatePriceMenuItemClick(product);
                    break;

                case ApplyDiscountMenuItemValue:
                    Console.WriteLine("Apply Discount menu item clicked");
                    HandleApplyDiscountMenuItemClick(product);
                    break;

                default:
                    throw new NotImplementedException("Unhandled menu item");
            }
        }

        private void HandlePriceHistoryMenuItemClick(ProductViewModel product)
        {
            var parameters = new Dictionary<string, object>
            {
                { nameof(ProductPriceHistory.ProductId), product.Id }
            };

            DialogService.Open<ProductPriceHistory>("Price History", parameters);
        }

        private void HandleUpdatePriceMenuItemClick(ProductViewModel product)
        {
            var parameters = new Dictionary<string, object>
            {
                { nameof(UpdateProductPriceForm.ProductId), product.Id },
                { nameof(UpdateProductPriceForm.ProductName), product.Name },
                { nameof(UpdateProductPriceForm.ProductPrice), product.Price },
                { nameof(UpdateProductPriceForm.OnSaveSuccessful), new EventCallback<int>(this, Initialize) },
            };

            DialogService.Open<UpdateProductPriceForm>("Update Price", parameters);
        }

        private void HandleApplyDiscountMenuItemClick(ProductViewModel product)
        {
            var parameters = new Dictionary<string, object>
            {
                { nameof(ApplyDiscountForm.ProductId), product.Id },
                { nameof(ApplyDiscountForm.ProductName), product.Name },
                { nameof(ApplyDiscountForm.ProductPrice), product.Price },
                { nameof(ApplyDiscountForm.OnSaveSuccessful), new EventCallback<int>(this, Initialize) },
            };

            DialogService.Open<ApplyDiscountForm>("Apply Discount", parameters);
        }
    }
}
