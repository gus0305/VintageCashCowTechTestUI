using System.Net;
using VintageCashCowTechTestUI.Client.Services;

namespace VintageCashCowTechTestUI.Client.Tests.Unit.Services
{
    [TestClass]
    public class HttpResponseMessageHandlerTests
    {
        private readonly HttpResponseMessageHandler _httpResponseMessageHandler;

        public HttpResponseMessageHandlerTests()
        {
            _httpResponseMessageHandler = new HttpResponseMessageHandler();
        }

        [TestMethod]
        public async Task Handle_WhenSuccessSuccessful_ReturnsContentAsRequiredType()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ \"Id\":100, \"Name\":\"TestName\" }")
            };

            // Act
            var result = await _httpResponseMessageHandler.Handle<HttpResponseMessageHandlerTestClass>(httpResponseMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(100, result.Id);
            Assert.AreEqual("TestName", result.Name);
        }

        [TestMethod]
        public async Task Handle_WhenBadRequest_ThrowsValidationException()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Invalid data")
            };

            // Act
            Exception? exception = null;
            try
            {
                await _httpResponseMessageHandler.Handle<HttpResponseMessageHandlerTestClass>(httpResponseMessage);
            }
            catch (ValidationException ve)
            {
                exception = ve;
            }

            // Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual("Invalid data", exception.Message);
        }

        [TestMethod]
        public async Task Handle_WhenUnsuccessfulRequest_ThrowsException()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            // Act
            Exception? exception = null;
            try
            {
                await _httpResponseMessageHandler.Handle<HttpResponseMessageHandlerTestClass>(httpResponseMessage);
            }
            catch (Exception ve)
            {
                exception = ve;
            }

            // Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual("Response status code does not indicate success: 500 (Internal Server Error).", exception.Message);
        }
    }
}