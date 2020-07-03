using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// inteface Nhà cung cấp
    /// </summary>
    
    public interface ISupplierDAL
    {
        /// <summary>
        /// Hiển thị nhà cung cấp (phân trang)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Supplier> List(int page, int pagesize, string searchValue);

        /// <summary>
        /// Đếm nhà cung cấp tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lấy nhà cung cấp theo id
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Supplier Get(int supplierID);

        /// <summary>
        /// Bổ sung 1 supplier, hàm trả về ID của supplier được bổ sung
        /// nếu lỗi, hàm trả về giá trị hỏ hơn hoặc bằng 0
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Supplier data);

        /// <summary>
        /// Cập nhật 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);

        /// <summary>
        /// Xóa nhiều supplier
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        int Delete(int[] supplierIDs);
    }
}
