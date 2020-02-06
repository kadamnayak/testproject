﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContradoSample.Models
{
    public class ProductModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<ProductAttributeModel> ProductAttributes { get; set; }
    }
}