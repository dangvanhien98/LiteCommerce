using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteComemerce.Admin.Models
{
    public class ShipperPaginationResult : PaginationResult
    {
        /// <summary>
        /// danh sách shipper
        /// </summary>
        public List<Shipper> Data { get; set; }
    }
}