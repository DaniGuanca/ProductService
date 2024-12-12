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
        private readonly CategoryClientService _categoryClientService;

        public ProductRepositoryService(ProductContext context, CategoryClientService categoryClientService)
        {
            _context = context;
            _categoryClientService = categoryClientService;
        }

        public async Task<ICollection<ProductDTO>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            var productsAndCategories = new List<ProductDTO>();

            foreach (var product in products) 
            { 
                var category = await _categoryClientService.GetCategoryAsync(product.CategoryId);
                productsAndCategories.Add(new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Stock = product.Stock,
                    Category = category
                });
            }
                        
            return productsAndCategories;
        }
        public async Task<ProductDTO?> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            var category = await _categoryClientService.GetCategoryAsync(product.CategoryId);

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Stock = product.Stock,
                Category= category
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
                Stock = _product.Stock,
            };

            var prodAdded = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return new ProductDTO
            {
                Id = prodAdded.Entity.Id,
                Stock = _product.Stock,
                Category = await _categoryClientService.GetCategoryAsync(prodAdded.Entity.CategoryId),
                Description = _product.Description,
                Name = _product.Name,
                Price = _product.Price,
            };
        }


        //ACA ME QUEDE BUSCAR EN UNA PAGINA COMO HACER EL UPDATE CON SQL SERVER ENTITY FRAWORK
        public async Task UpdateProduct(int id, UpdateProductDTO productDTO)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            
            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado");

            product.CategoryId = productDTO.CategoryId;
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Stock = productDTO.Stock;            
            product.Price = productDTO.Price;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado");

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}
