using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteComemerce.Admin.Models
{
    public class ProductAttributePaginationResult : PaginationResult
    {
        public string ProductID { get; set; }
        public List<ProductAttribute> Data { get; set; }
    }
}