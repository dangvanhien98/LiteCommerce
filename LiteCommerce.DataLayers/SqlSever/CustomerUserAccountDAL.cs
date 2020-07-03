using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SqlSever
{
    public class CustomerUserAccountDAL : IUserAccountDAL
    {
        private string connectionString;

        public CustomerUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public UserAccount Authorize(string userName, string password)
        {
            //TODO: Kiểm tra thông tin đăng nhập tư bảng employee
            return new UserAccount()
            {
                UserID = userName,
                FullName = "Đặng Văn Hiền",
                Photo = "abc.jpg"
            };
        }

    }
}
