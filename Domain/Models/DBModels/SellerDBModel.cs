﻿using Domain.Models.Primitives;

namespace Domain.Models.DBModels
{
    public class SellerDBModel : EntityDBModel
    {
        public string ApiKey { get; set; }
        public string StoreName { get; set; }
        public string? LogoImageUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsActive { get; set; }
        public decimal AccoundBalance { get; set; }

        public int UserId { get; set; }
        public ApplicationUserDBModel User { get; set; }

        public ICollection<SellerProductDetailsDBModel> Prices { get; set; }
        public ICollection<PriceHistoryDBModel> PriceHistories { get; set; }
        public ICollection<ProductSellerLinkDBModel> ProductLinks { get; set; }
    }

}
