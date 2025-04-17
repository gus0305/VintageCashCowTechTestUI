using VintageCashCowTechTestUI.Client.Mappers;
using VintageCashCowTechTestUI.Client.Services.Product.DataContracts;

namespace VintageCashCowTechTestUI.Client.Tests.Unit.Mappers
{
    [TestClass]
    public sealed class ProductViewModelMapperTests
    {
        private ProductViewModelMapper _productViewModelMapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _productViewModelMapper = new ProductViewModelMapper();
        }

        [TestMethod]
        public void Map_ReturnsMappedItems()
        {
            // Arrange            
            var products = new List<ProductResponse>
            {
                new ProductResponse
                {
                    Id = 1,
                    Name = "Test1",
                    Price = 1.11m,
                    LastUpdated = new DateTime(2021, 12, 13)
                },
                new ProductResponse
                {
                    Id = 2,
                    Name = "Test2",
                    Price = 2.22m,
                    LastUpdated = new DateTime(2022, 11, 15)
                }
            };

            // Act
            var result = _productViewModelMapper.Map(products);

            // Assert
            Assert.IsNotNull(result, "result");
            Assert.AreEqual(2, result.Count, "result.Count");

            var firstProduct = result[0];
            Assert.AreEqual(1, firstProduct.Id, "firstProduct.Id");
            Assert.AreEqual("Test1", firstProduct.Name, "firstProduct.Name");
            Assert.AreEqual(1.11m, firstProduct.Price, "firstProduct.Price");
            Assert.AreEqual(new DateTime(2021, 12, 13), firstProduct.LastUpdated, "firstProduct.LastUpdated");

            var secondProduct = result[1];
            Assert.AreEqual(2, secondProduct.Id, "secondProduct.Id");
            Assert.AreEqual("Test2", secondProduct.Name, "secondProduct.Name");
            Assert.AreEqual(2.22m, secondProduct.Price, "secondProduct.Price");
            Assert.AreEqual(new DateTime(2022, 11, 15), secondProduct.LastUpdated, "secondProduct.LastUpdated");
        }

    }
}
