using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteComemerce.Admin.Models
{
    public abstract class PaginationResult
    {
        /// <summary>
        /// số trang hiện tại
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// tổng số trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// số dòng tìm kiếm được
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// giá trị tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }

        /// <summary>
        /// tổng số trang
        /// </summary>
        public int PageCount
        {
            get
            {
                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    p += 1;
                return p;
            }
        }

    }
}