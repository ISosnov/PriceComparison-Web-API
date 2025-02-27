﻿using Domain.Models.Response.Gpt.Product;

namespace Domain.Models.Response.Gpt
{
    public class GptComparisonProductCharacteristicResponseModel
    {
        public string? ProductATitle { get; set; }
        public string? ProductBTitle { get; set; }
        public IEnumerable<SimplifiedProductCharacteristicGroupResponseModel>? ProductA { get; set; }
        public IEnumerable<SimplifiedProductCharacteristicGroupResponseModel>? ProductB { get; set; }
        public string? Explanation { get; set; }
    }
}
