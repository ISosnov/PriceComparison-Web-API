﻿using Domain.Models.Primitives;

namespace Domain.Models.DBModels
{
    public class SellerDBModel : IEntity<int>
    {
        public int Id { get; set; }
        public string ApiKey { get; set; }
        public string StoreName { get; set; }
        public string? LogoImageUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsActive { get; set; }
        public decimal AccountBalance { get; set; }
        public bool PublishPriceList { get; set; }

        public int UserId { get; set; }
        public ApplicationUserDBModel User { get; set; }

        public ICollection<SellerProductDetailsDBModel> SellerProductDetails { get; set; }
        public ICollection<PriceHistoryDBModel> PriceHistories { get; set; }
        public ICollection<ProductReferenceClickDBModel> ProductSellerReferenceClicks { get; set; }
        public ICollection<AuctionClickRateDBModel> AuctionClickRates { get; set; }
    }

}
