using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Radzen;
using VintageCashCowTechTestUI.Client.Components;
using VintageCashCowTechTestUI.Client.Mappers;
using VintageCashCowTechTestUI.Client.Services.Product;
using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;
using VintageCashCowTechTestUI.Client.ViewModels;

namespace VintageCashCowTechTestUI.Client.Tests.Unit.Components
{
    [TestClass]
    public sealed class ProductListTests : BunitTestContext
    {
        [TestMethod]
        public void OnInitializeAsync_SetsTableData()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var productViewModelMapperMock = new Mock<IProductViewModelMapper>();

            Services.AddSingleton(productServiceMock.Object);
            Services.AddSingleton(productViewModelMapperMock.Object);
            Services.AddSingleton(productViewModelMapperMock.Object);
            Services.AddSingleton(typeof(ContextMenuService));
            Services.AddSingleton(typeof(DialogService));

            var products = new List<ProductResponse>();
            productServiceMock.Setup(x => x.GetAllProductsAsync()).Returns(Task.FromResult(products));

            var viewModelProducts = new List<ProductViewModel>{
                new ProductViewModel
                {
                    Id = 11,
                    LastUpdated = new DateTime(2025, 4, 17, 23 , 59, 58),
                    Name = "Product1",
                    Price = 123.7m
                },
                new ProductViewModel
                {
                    Id = 22,
                    LastUpdated = new DateTime(2022, 4, 17, 23, 59, 22),
                    Name = "Product22",
                    Price = 123.22m
                }
            };
            productViewModelMapperMock.Setup(x => x.Map(products)).Returns(viewModelProducts);

            // Act
            var cut = TestContext!.RenderComponent<ProductList>();

            // Assert
            var firstRowProductIdElement = cut.Find(GetTableCellSelector(1, 1));
            Assert.AreEqual("11", firstRowProductIdElement.TextContent, "firstRowProductId");

            var firstRowProductNameElement = cut.Find(GetTableCellSelector(1, 2));
            Assert.AreEqual("Product1", firstRowProductNameElement.TextContent, "firstRowProductName");

            var firstRowPriceElement = cut.Find(GetTableCellSelector(1, 3));
            Assert.AreEqual("£123.70", firstRowPriceElement.TextContent, "firstRowPrice");

            var firstRowLastUpdatedElement = cut.Find(GetTableCellSelector(1, 4));
            Assert.AreEqual("17/04/2025 23:59:58", firstRowLastUpdatedElement.TextContent, "firstRowLastUpdated");

            var secondRowProductIdElement = cut.Find(GetTableCellSelector(2, 1));
            Assert.AreEqual("22", secondRowProductIdElement.TextContent, "secondRowProductId");

            var secondRowProductNameElement = cut.Find(GetTableCellSelector(2, 2));
            Assert.AreEqual("Product22", secondRowProductNameElement.TextContent, "secondRowProductName");

            var secondRowPriceElement = cut.Find(GetTableCellSelector(2, 3));
            Assert.AreEqual("£123.22", secondRowPriceElement.TextContent, "secondRowPrice");

            var secondRowLastUpdatedElement = cut.Find(GetTableCellSelector(2, 4));
            Assert.AreEqual("17/04/2022 23:59:22", secondRowLastUpdatedElement.TextContent, "secondRowLastUpdated");
        }
    }
}
