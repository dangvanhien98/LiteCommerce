using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SqlSever
{
    public class OrderDetailNewDAL : IOrderDetailNewDAL
    {
        private string connectionString;

        /// <summary>
        /// construct
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderDetailNewDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Add(OrderDetailNew data)
        {
            int orderId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO OrderDetails
                                          (
                                              OrderID,
	                                          ProductID,
	                                          UnitPrice,
	                                          Quantity,
                                              Discount
                                          )
                                          VALUES
                                          (
                                              @OrderID,
	                                          @ProductID,
	                                          @UnitPrice,
	                                          @Quantity,
                                              @Discount
                                          );";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@OrderID", data.OrderID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@UnitPrice", data.UnitPrice);
                cmd.Parameters.AddWithValue("@Quantity", data.Quantity);
                cmd.Parameters.AddWithValue("@Discount", data.Discount);
                orderId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return orderId;
        }
    }
}
