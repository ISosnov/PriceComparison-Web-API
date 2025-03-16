﻿namespace Domain.Models.Response.Products
{
    public class BaseProductResponseModel
    {
        public int BaseProductId { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<ProductGroupResponseModel> ProductGroups { get; set; }
    }
}
