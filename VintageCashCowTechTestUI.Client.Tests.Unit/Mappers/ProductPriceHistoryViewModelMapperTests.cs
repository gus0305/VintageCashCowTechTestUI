using VintageCashCowTechTestUI.Client.Mappers;
using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;

namespace VintageCashCowTechTestUI.Client.Tests.Unit.Mappers
{
    [TestClass]
    public sealed class ProductPriceHistoryViewModelMapperTests
    {
        private ProductPriceHistoryViewModelMapper _productPriceHistoryViewModelMapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _productPriceHistoryViewModelMapper = new ProductPriceHistoryViewModelMapper();
        }

        [TestMethod]
        public void Map_ReturnsMappedItems()
        {
            // Arrange            
            var product = new ProductPriceHistoryResponse
            {
                Id = 1,
                Name = "Test1",
                PriceHistory = new List<PriceHistory>
                {
                    new PriceHistory
                    {
                        Date = new DateTime(2021, 11, 12),
                        Price = 1.11m
                    },
                    new PriceHistory
                    {
                        Date = new DateTime(2022, 10, 15),
                        Price = 2.22m
                    }
                }
            };

            // Act
            var result = _productPriceHistoryViewModelMapper.Map(product);

            // Assert
            Assert.IsNotNull(result, "result");

            Assert.AreEqual(1, result.Id, "result.Id");
            Assert.AreEqual("Test1", result.Name, "result.Name");

            Assert.AreEqual(2, result.PriceHistory.Count, "PriceHistory.Count");

            var firstPriceHistory = result.PriceHistory[0];
            Assert.AreEqual(1.11m, firstPriceHistory.Price, "firstPriceHistory.Price");
            Assert.AreEqual(new DateTime(2021, 11, 12), firstPriceHistory.Date, "firstPriceHistory.Date");

            var secondPriceHistory = result.PriceHistory[1];
            Assert.AreEqual(2.22m, secondPriceHistory.Price, "secondPriceHistory.Price");
            Assert.AreEqual(new DateTime(2022, 10, 15), secondPriceHistory.Date, "secondPriceHistory.Date");
        }

    }
}
