using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// khách hàng
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// hiển thị khách hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Customer> List(int page, int pagesize, string searchValue, string country);

        /// <summary>
        /// đếm khách hàng tìm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue, string country);

        Customer Get(string customerID);

        int Add(Customer data);

        bool Update(Customer data);

        int Delete(string[] customerIDs);
    }
}
