using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteComemerce.Admin.Models
{
    public class CustomerPaginationResult : PaginationResult
    {
        public string Coutries { get; set; }
        /// <summary>
        /// Danh sách Customer
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}