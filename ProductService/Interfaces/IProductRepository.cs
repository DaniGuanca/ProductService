using ProductService.Data;
using ProductService.DTOs;

namespace ProductService.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<ProductDTO>> GetProducts();
        Task<ProductDTO?> GetProduct(int id);
        Task<ProductDTO> CreateProduct(AddProductDTO product);
        Task UpdateProduct(int id, UpdateProductDTO product);
        Task DeleteProduct(int id);
    }
}
