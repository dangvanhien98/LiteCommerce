using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// kiểm tra us va pass có  hợp lệ không
    /// hợp lệ trả về info user
    /// ngược lại trả về null
    /// </summary>

    public interface IUserAccountDAL
    {
        UserAccount Authorize(string userName, string password);
    }
}
