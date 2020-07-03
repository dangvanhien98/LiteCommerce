using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IShipperDAL
    {
        /// <summary>
        /// Hiển thị người giao hàng (phân trang)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Shipper> List(int page, int pagesize, string searchValue);

        /// <summary>
        /// Đếm người giao hàng tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        Shipper Get(int shipperID);

        int Add(Shipper data);

        bool Update(Shipper data);

        int Delete(int[] shipperIDs);
    }
}
