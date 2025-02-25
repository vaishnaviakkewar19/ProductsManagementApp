using ProductsManagementApp.Models;

namespace ProductsManagementApp.Interfaces
{
    public interface IProductService
    {
        Task AddProduct(Product product);
    }
}
