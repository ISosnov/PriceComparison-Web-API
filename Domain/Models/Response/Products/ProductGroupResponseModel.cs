﻿namespace Domain.Models.Response.Products
{
    public class ProductGroupResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductGroupTypeId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
