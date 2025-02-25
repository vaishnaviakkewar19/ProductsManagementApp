using Microsoft.EntityFrameworkCore;
using ProductsManagementApp.Interfaces;
using ProductsManagementApp.Models;
using ProductsManagementApp.Services;

namespace ProductsManagementApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private async Task<int> GenerateUniqueProductId()
        {
            int newId;
            bool isUnique;
            var random = new Random();

            do
            {
                newId = random.Next(100000, 999999); // Generates a 6-digit number
                var products = await _productRepository.GetProducts();
                isUnique = products!=null ? !products.Any(p => p.Id.Equals(newId)) : false;
            } while (!isUnique);

            return newId;
        }

        public async Task AddProduct(Product product)
        {
            product.Id = await GenerateUniqueProductId();
            await _productRepository.AddProduct(product);
        }

    }
}
