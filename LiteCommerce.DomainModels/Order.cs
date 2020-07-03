using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// đơn đặt hàng
    /// </summary>
    public class Order
    {
        public int OrderID { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string Status { get; set; }       
    }
}
