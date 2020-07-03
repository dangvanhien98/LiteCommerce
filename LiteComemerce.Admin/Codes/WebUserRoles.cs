using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteComemerce.Admin
{
    /// <summary>
    /// Định nghĩa danh sách các Role của user
    /// </summary>
    public class WebUserRoles
    {
        /// <summary>
        /// Không xác định
        /// </summary>
        public const string ANONYMOUS = "anonymous";
        /// <summary>
        /// Nhân viên
        /// </summary>
        public const string STAFF = "Saleman";
        /// <summary>
        /// Quản trị hệ thống
        /// </summary>
        public const string ADMINISTRATOR = "administrator";
        /// <summary>
        /// Quản trị tài khoản
        /// </summary>
        public const string ACCOUNTADMIN = "Accountadmin";
    }
}