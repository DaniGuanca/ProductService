using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.DTOs;
using ProductService.Interfaces;
using ProductService.Models;

namespace ProductService.Services
{
    public class ProductRepositoryService : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepositoryService(ProductContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ProductDTO>> GetProducts()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();

            var productsDTO = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.Category.Id,
                CategoryName = p.Category.Name,
                Stock = p.Stock
            }).ToList();

            return productsDTO;
        }
        public async Task<ProductDTO?> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name,
                Stock = product.Stock
            };

            return productDTO;
        }

        public async Task<ProductDTO> CreateProduct(AddProductDTO _product)
        {
            var product = new Product
            {
                Name = _product.Name,
                Price = _product.Price,
                Description = _product.Description,
                CategoryId = _product.CategoryId,
                Stock = _product.Stock
            };

            var prodAdded = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return new ProductDTO
            {
                Id = prodAdded.Entity.Id,
                Stock = _product.Stock,
                CategoryId = _product.CategoryId,
                Description = _product.Description,
                Name = _product.Name,
                Price = _product.Price,
            };
        }


        //ACA ME QUEDE BUSCAR EN UNA PAGINA COMO HACER EL UPDATE CON SQL SERVER ENTITY FRAWORK
        public async Task UpdateProduct(int id, UpdateProductDTO product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
