using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LiteComemerce.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessLayerConfig
    {
        public static void Init()
        {
            string connectitonString = ConfigurationManager.ConnectionStrings["LiteCommerce"].ConnectionString;

            //TODO: Khởi tạo các BLL khi cần sử dụng
            CatalogBLL.Initialize(connectitonString);
            UserAccountBLL.Initialize(connectitonString);
        }
    }
}