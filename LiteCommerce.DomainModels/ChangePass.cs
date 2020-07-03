using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class ChangePass
    {
        public int EmployeeID { get; set; }
        public string Email { get; set; }
        public string OldPassWord { get; set; }
        public string NewPassWord { get; set; }
        public string EnterAgain { get; set; }
    }
}
