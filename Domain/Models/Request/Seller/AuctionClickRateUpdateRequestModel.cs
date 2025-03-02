﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Request.Seller
{
    public class AuctionClickRateUpdateRequestModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public decimal ClickRate { get; set; }
    }
}
