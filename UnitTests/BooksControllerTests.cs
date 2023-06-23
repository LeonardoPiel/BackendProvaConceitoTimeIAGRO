using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class BooksControllerTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            // Configurar o HttpClient para se comunicar com a API de testes
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5150/");
        }

        [Test]
        public async Task GetDeliveryTax_WithValidBookId_ReturnsTax()
        {
            // Arrange
            int bookId = 1;

            // Act
            var response = await _httpClient.GetAsync($"Books/{bookId}/GetDeliveryTax");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(jsonResponse);

            Assert.True(document.RootElement.TryGetProperty("data", out var dataElement));
            Assert.True(dataElement.TryGetProperty("tax", out var taxElement));
            Assert.AreEqual(2, taxElement.GetInt32());
        }

        [Test]
        public async Task GetDeliveryTax_WithInvalidBookId_ReturnsNotFound()
        {
            // Arrange
            int bookId = 99;

            // Act
            var response = await _httpClient.GetAsync($"Books/{bookId}/GetDeliveryTax");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
