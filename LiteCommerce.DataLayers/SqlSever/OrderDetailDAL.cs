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
    public class OrderDetailDAL : IOrderDetailDAL
    {
        private string connectionString;

        /// <summary>
        /// construct
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderDetailDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<OrderDetail> ListDetail(int id)
        {
            List<OrderDetail> data = new List<OrderDetail>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //chuẩn bị câu lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT        OrderDetails.OrderID, Products.ProductName, OrderDetails.UnitPrice, OrderDetails.Quantity, OrderDetails.Discount
                                    FROM            OrderDetails INNER JOIN
                                                             Products ON OrderDetails.ProductID = Products.ProductID
                                    WHERE          OrderID = @OrderID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@OrderID", id);
                //thực thi câu lệnh
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new OrderDetail()
                        {                          
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            UnitPrice = Convert.ToDouble(dbReader["UnitPrice"]),
                            Quantity = Convert.ToInt32(dbReader["Quantity"]),
                            Discount = Convert.ToDouble(dbReader["Discount"]),
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }
    }
}
