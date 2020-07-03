using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteComemerce.Admin.Models
{
    public class CategoriePaginationResult : PaginationResult
    {
        /// <summary>
        /// danh sách danh mục
        /// </summary>
        public List<Categorie> Data { get; set; } 
    }
}