using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContradoSample.Models
{
    public class ProductListModel
    {
        public List<ProductModel> List { get; set; }
        public int TotalRecords { get; set; }
    }
}