﻿namespace Domain.Models.Request.Products
{
    public class CharacteristicCreateRequestModel
    {
        public string Title { get; set; }
        public string DataType { get; set; }
        public string? Unit { get; set; }
    }
}
