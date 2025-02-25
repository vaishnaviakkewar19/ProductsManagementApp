using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductsManagementApp.Interfaces;
using ProductsManagementApp.Models;
using ProductsManagementApp.Services;

namespace ProductManagementAppTest
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductsController(_mockProductRepository.Object, _mockProductService.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10, Stock = 100 },
                new Product { Id = 2, Name = "Product 2", Price = 20, Stock = 200 },
            };
            _mockProductRepository.Setup(repo => repo.GetProducts()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 10, Stock = 100 };
            _mockProductRepository.Setup(repo => repo.GetProduct(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(product.Name, returnValue.Name);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetProduct(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 10, Stock = 100 };

            // Act
            var result = await _controller.AddProduct(product);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Product>(createdAtActionResult.Value);
            Assert.Equal(product.Name, returnValue.Name);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 10, Stock = 100 };
            _mockProductRepository.Setup(repo => repo.UpdateProduct(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProduct(1, product);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 10, Stock = 100 };

            // Act
            var result = await _controller.UpdateProduct(2, product);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.DeleteProduct(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DecrementStock_ReturnsOk()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.DecrementStock(1, 10)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DecrementStock(1, 10);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddToStock_ReturnsOk()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.AddToStock(1, 10)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddToStock(1, 10);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}