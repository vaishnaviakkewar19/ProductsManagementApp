using ProductsManagementApp.Models;

namespace ProductsManagementApp.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task DecrementStock(int id, int quantity);
        Task AddToStock(int id, int quantity);
    }
}
