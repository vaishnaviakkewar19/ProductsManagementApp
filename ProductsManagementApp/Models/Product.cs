using System.ComponentModel.DataAnnotations;

namespace ProductsManagementApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
