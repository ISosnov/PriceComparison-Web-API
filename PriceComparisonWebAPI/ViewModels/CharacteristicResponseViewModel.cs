﻿namespace PriceComparisonWebAPI.ViewModels
{
    public class CharacteristicResponseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DataType { get; set; }
        public string? Unit { get; set; }
    }
}