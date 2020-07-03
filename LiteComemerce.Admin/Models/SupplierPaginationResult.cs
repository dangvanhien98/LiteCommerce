using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteComemerce.Admin.Models
{
    public class SupplierPaginationResult : PaginationResult
    {
        /// <summary>
        /// Danh sách supplier
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}