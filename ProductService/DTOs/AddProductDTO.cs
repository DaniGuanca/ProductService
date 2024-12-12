﻿namespace ProductService.DTOs
{
    public class AddProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int Stock {  get; set; }
    }
}