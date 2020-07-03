using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IProductDAL
    {
        /// <summary>
        /// Hiển thị product (phân trang)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Product> List(int page, int pagesize, string searchValue, string supplier, string category);

        /// <summary>
        /// count
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue, string supplier, string category);

        Product Get(int productID);

        int Add(Product data);

        bool Update(Product data);

        int Delete(int[] productIDs);
    }
}
