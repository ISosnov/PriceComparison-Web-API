﻿namespace Domain.Models.Response.Products
{
    public class ProductResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}
