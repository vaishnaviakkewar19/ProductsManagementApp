using Microsoft.AspNetCore.Mvc;
using ProductsManagementApp.Interfaces;
using ProductsManagementApp.Models;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;

    public ProductsController(IProductRepository productRepository, IProductService productService)
    {
        _productRepository = productRepository;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return Ok(await _productRepository.GetProducts());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productRepository.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] Product product)
    {
        await _productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }
        await _productRepository.UpdateProduct(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productRepository.DeleteProduct(id);
        return NoContent();
    }

    [HttpPut("decrement-stock/{id}/{quantity}")]
    public async Task<IActionResult> DecrementStock(int id, int quantity)
    {
        await _productRepository.DecrementStock(id, quantity);
        return Ok();
    }

    [HttpPut("add-to-stock/{id}/{quantity}")]
    public async Task<IActionResult> AddToStock(int id, int quantity)
    {
        await _productRepository.AddToStock(id, quantity);
        return Ok();
    }
}
