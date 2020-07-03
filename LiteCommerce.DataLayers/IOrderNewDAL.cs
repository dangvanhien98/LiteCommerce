using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IOrderNewDAL
    {
        List<OrderNew> List(int page, int pagesize, string searchValue);

        /// <summary>
        /// đếm danh mục tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
      
        int Add(OrderNew data);
      
       
    }
}
