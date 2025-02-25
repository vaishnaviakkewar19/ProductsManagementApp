using Microsoft.EntityFrameworkCore;
using ProductsManagementApp.Interfaces;
using ProductsManagementApp.Models;

namespace ProductsManagementApp.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext _context;

        public ProductRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products?.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProduct(Product product)
        {           
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DecrementStock(int id, int quantity)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Stock -= quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddToStock(int id, int quantity)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Stock += quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
