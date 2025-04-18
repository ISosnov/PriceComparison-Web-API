﻿using Domain.Models.Primitives;

namespace Domain.Models.DBModels
{
    public class ProductVideoDBModel : IEntity<int>
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public int BaseProductId { get; set; }

        public BaseProductDBModel BaseProduct { get; set; }
    }
}
