using AngleSharp.Dom;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Radzen;
using VintageCashCowTechTestUI.Client.Components;
using VintageCashCowTechTestUI.Client.Services;
using VintageCashCowTechTestUI.Client.Services.Product;

namespace VintageCashCowTechTestUI.Client.Tests.Unit.Components
{
    [TestClass]
    public sealed class ApplyDiscountFormTests : BunitTestContext
    {
        private const int productId = 1234;
        private const string productName = "ProductName 1234";
        private const decimal productPrice = 99.45m;

        private IRenderedComponent<ApplyDiscountForm> RenderComponent(int productId, string productName, decimal productPrice)
        {
            var component = TestContext!.RenderComponent<ApplyDiscountForm>(parameters => parameters
                .Add(p => p.ProductId, productId)
                .Add(p => p.ProductName, productName)
                .Add(p => p.ProductPrice, productPrice));
            return component;   
        }

        [TestMethod]
        public void OnInitialize_SetsFormData()
        {
            // Arrange
            Services.AddSingleton(new Mock<IProductService>().Object);
            Services.AddSingleton(typeof(DialogService));

            // Act
            var cut = RenderComponent(productId, productName, productPrice);

            // Assert
            var inputProductIdElement = GetInputProductIdElement(cut);
            Assert.AreEqual("1234", inputProductIdElement.GetAttribute("value"), "inputProductId");

            var inputProductNameElement = GetInputProductNameElement(cut);
            Assert.AreEqual("ProductName 1234", inputProductNameElement.GetAttribute("value"), "inputProductName");

            var inputProducPriceElement = GetInputProductPriceElement(cut);
            Assert.AreEqual("99.45", inputProducPriceElement.GetAttribute("value"), "inputProductPrice");
        }

        private static IElement GetInputProductIdElement(IRenderedComponent<ApplyDiscountForm> cut)
        {
            return cut.Find("div > div > div:nth-child(1) > div > span > input");
        }

        private static IElement GetInputProductNameElement(IRenderedComponent<ApplyDiscountForm> cut)
        {
            return cut.Find("div > div > div:nth-child(2) > div > input");
        }

        private static IElement GetInputProductPriceElement(IRenderedComponent<ApplyDiscountForm> cut)
        {
            return cut.Find("div > div > div:nth-child(3) div > span > input");
        }

        private static IElement GetSaveButtonElement(IRenderedComponent<ApplyDiscountForm> component)
        {
            return component.Find("div > div > div:last-child > button");
        }

        private static IElement GetDiscountPercentageInputElement(IRenderedComponent<ApplyDiscountForm> component)
        {
            return component.Find("div > div > div:nth-child(4) div > span > input");
        }

        private static IElement GetServerErrorElement(IRenderedComponent<ApplyDiscountForm> component)
        {
            return component.Find("div > div > div");
        }

        [TestMethod]
        public void OnSaveClicked_WhenDataValid_AppliesDiscount()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            Services.AddSingleton(productServiceMock.Object);
            Services.AddSingleton(typeof(DialogService));

            var cut = RenderComponent(productId, productName, productPrice);

            const int discountPercentage = 35;
            var discountPercentageElement = GetDiscountPercentageInputElement(cut);
            discountPercentageElement.Change(discountPercentage.ToString());

            // Act
            GetSaveButtonElement(cut).Click();

            // Assert
            productServiceMock.Verify(x => x.ApplyDiscountAsync(productId, discountPercentage), Times.Exactly(1));
        }

        [TestMethod]
        public void OnSaveClicked_WhenDataInvalid_SetsError()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            Services.AddSingleton(productServiceMock.Object);
            Services.AddSingleton(typeof(DialogService));

            var cut = RenderComponent(productId, productName, productPrice);

            var validationException = new ValidationException("Validation exception from ApplyDiscountAsync");
            productServiceMock.Setup(x => x.ApplyDiscountAsync(productId, It.IsAny<int>())).ThrowsAsync(validationException);

            // Act
            GetSaveButtonElement(cut).Click();

            // Assert
            var severErrorElement = GetServerErrorElement(cut);
            Assert.AreEqual("Validation exception from ApplyDiscountAsync", severErrorElement.TextContent); 
        }
    }
}
