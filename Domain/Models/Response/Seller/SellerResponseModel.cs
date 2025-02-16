﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Response.Seller
{
    public class SellerResponseModel
    {
        public int Id { get; set; }
        public string ApiKey { get; set; }
        public string? LogoImageUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
