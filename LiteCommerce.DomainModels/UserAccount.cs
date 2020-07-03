using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Lưu thông tin liên quan đến tài khoản đăng nhập
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// ID của tài khoản đăng nhập
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// tên đầy đủ của first name và last name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// tên file ảnh của user
        /// </summary>
        public string Photo { get; set; }

        public string Title { get; set; }

        public string Roles { get; set; }

    }
}
