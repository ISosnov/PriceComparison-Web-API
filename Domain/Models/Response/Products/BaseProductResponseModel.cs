﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Response.Products
{
    public class BaseProductResponseModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsUnderModeration { get; set; }
        public DateTime AddedToDatabase { get; set; }
        public int CategoryId { get; set; }
    }
}
