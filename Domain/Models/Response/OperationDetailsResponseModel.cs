﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Response
{
    public class OperationDetailsResponseModel
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public Exception Exception { get; set; }
    }
}
