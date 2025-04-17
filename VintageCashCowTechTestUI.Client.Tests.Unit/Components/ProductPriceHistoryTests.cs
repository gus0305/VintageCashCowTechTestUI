using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using VintageCashCowTechTestUI.Client.Components;
using VintageCashCowTechTestUI.Client.Mappers;
using VintageCashCowTechTestUI.Client.Services.Product;
using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;
using VintageCashCowTechTestUI.Client.ViewModels;

namespace VintageCashCowTechTestUI.Client.Tests.Unit.Components
{
    [TestClass]
    public sealed class ProductPriceHistoryTests : BunitTestContext
    {
        [TestMethod]
        public void OnInitializeAsync_SetsData()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var productPriceHistoryViewModelMapperMock = new Mock<IProductPriceHistoryViewModelMapper>();

            Services.AddSingleton(productServiceMock.Object);
            Services.AddSingleton(productPriceHistoryViewModelMapperMock.Object);

            const int productId = 1234;
            var product = new ProductPriceHistoryResponse();
            productServiceMock.Setup(x => x.GetProductAsync(productId)).Returns(Task.FromResult(product));

            var productPriceHistoryViewModel = new ProductPriceHistoryViewModel
            {
                Id = productId,
                Name = $"ProductName 1234",
                PriceHistory = new List<PriceHistoryViewModel>
                {
                    new PriceHistoryViewModel
                    {
                        Date = new DateTime(2025, 4, 17, 23 , 59, 58),
                        Price = 123.7m
                    },
                    new PriceHistoryViewModel
                    {
                        Date = new DateTime(2022, 4, 17, 23, 59, 22),
                        Price = 123.22m
                    }
                }
            };
            productPriceHistoryViewModelMapperMock.Setup(x => x.Map(product)).Returns(productPriceHistoryViewModel);

            // Act
            var cut = TestContext!.RenderComponent<ProductPriceHistory>(parameters => parameters.Add(p => p.ProductId, productId));

            // Assert
            var inputProductIdElement = cut.Find("div > div > div:nth-child(1) > div > span > input");
            Assert.AreEqual("1234", inputProductIdElement.GetAttribute("value"), "inputProductId");

            var inputProductNameElement = cut.Find("div > div > div:nth-child(2) > div > input");
            Assert.AreEqual("ProductName 1234", inputProductNameElement.GetAttribute("value"), "inputProductName");

            var firstRowDateElement = cut.Find(GetTableCellSelector(1, 1));
            Assert.AreEqual("17/04/2025 23:59:58", firstRowDateElement.TextContent, "firstRowDate");

            var firstRowPriceElement = cut.Find(GetTableCellSelector(1, 2));
            Assert.AreEqual("£123.70", firstRowPriceElement.TextContent, "firstRowPrice");

            var secondRowDateElement = cut.Find(GetTableCellSelector(2, 1));
            Assert.AreEqual("17/04/2022 23:59:22", secondRowDateElement.TextContent, "secondRowDate");

            var secondRowPriceElement = cut.Find(GetTableCellSelector(2, 2));
            Assert.AreEqual("£123.22", secondRowPriceElement.TextContent, "secondRowPrice");
        }
    }
}
