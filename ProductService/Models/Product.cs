using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int Stock {  get; set; }
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
    }
}
