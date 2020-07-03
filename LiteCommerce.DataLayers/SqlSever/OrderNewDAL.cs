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
    public class OrderNewDAL : IOrderNewDAL
    {
        private string connectionString;

        /// <summary>
        /// construct
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderNewDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(OrderNew data)
        {
            int orderId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Orders
                                          (	                                     
	                                          CustomerID,
                                              EmployeeID,
	                                          OrderDate,
                                              RequiredDate,
	                                          ShippedDate,
                                              ShipperID,
	                                          Freight,
                                              ShipAddress,
	                                          ShipCity,
                                              ShipCountry,
                                              Status	                                          
                                          )
                                          VALUES
                                          (
	                                          @CustomerID,
                                              @EmployeeID,
	                                          @OrderDate,
                                              @RequiredDate,
	                                          @ShippedDate,
                                              @ShipperID,
	                                          @Freight,
                                              @ShipAddress,
	                                          @ShipCity,
                                              @ShipCountry,
                                              @Status
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@OrderDate", data.OrderDate);
                cmd.Parameters.AddWithValue("@RequiredDate", data.RequiredDate);
                cmd.Parameters.AddWithValue("@ShippedDate", data.ShippedDate);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@Freight", data.Freight);
                cmd.Parameters.AddWithValue("@ShipAddress", data.ShipAddress);
                cmd.Parameters.AddWithValue("@ShipCity", data.ShipCity);
                cmd.Parameters.AddWithValue("@ShipCountry", data.ShipCountry);
                cmd.Parameters.AddWithValue("@Status", "queue");

                orderId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return orderId;
        }

        public int Count(string searchValue)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //chuẩn bị câu lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select count(*) from Orders 
                                                    where (Status = 'queue') and @searchValue = N'' or ShipCountry like @searchValue	";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return count;
        }

        public List<OrderNew> List(int page, int pageSize, string searchValue)
        {
            List<OrderNew> data = new List<OrderNew>();

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //chuẩn bị câu lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from (select *, ROW_NUMBER() over(order by OrderDate) as RowNumber
                                                    from Orders
                                                    where (Status = 'queue') and (@searchValue = N'') or (ShipCountry like @searchValue))as t
                                                    where t.RowNumber between (@page-1)*@pageSize+1 and @page*@pageSize
                                                    order by t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                //thực thi câu lệnh
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new OrderNew()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            CustomerID = Convert.ToString(dbReader["CustomerID"]),
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            OrderDate = Convert.ToDateTime(dbReader["OrderDate"]),
                            RequiredDate = Convert.ToDateTime(dbReader["RequiredDate"]),
                            ShippedDate = Convert.ToDateTime(dbReader["ShippedDate"]),
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            Freight = Convert.ToDouble(dbReader["Freight"]),
                            ShipAddress = Convert.ToString(dbReader["ShipAddress"]),
                            ShipCity = Convert.ToString(dbReader["ShipCity"]),
                            ShipCountry = Convert.ToString(dbReader["ShipCountry"]),
                            Status = Convert.ToString(dbReader["Status"]),
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }
    }
}
