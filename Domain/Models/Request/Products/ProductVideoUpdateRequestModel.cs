﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Request.Products
{
    public class ProductVideoUpdateRequestModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string VideoUrl { get; set; }
    }
}
