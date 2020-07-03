using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IOrderDAL
    {
        List<Order> List();

        List<Order> ListAccepted();

        int Delete(int[] OrderIDs);

        int DeleteOrder(int[] OrderIDs);

        bool Update(int OrderID);

    }
}
