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
    public class OrderDAL : IOrderDAL
    {
        private string connectionString;

        /// <summary>
        /// construct
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Delete(int[] OrderIDs)
        {
            int countDeleted = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM OrderDetails WHERE(OrderID = @orderID)";              
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@orderID", SqlDbType.Int);
                foreach (int orderIDs in OrderIDs)
                {
                    cmd.Parameters["@orderID"].Value = orderIDs;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        countDeleted += 1;
                }

                connection.Close();
            }
            return countDeleted;
        }

        public int DeleteOrder(int[] OrderIDs)
        {
            int countDeleted = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Orders WHERE(OrderID = @orderID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@orderID", SqlDbType.Int);
                foreach (int orderIDs in OrderIDs)
                {
                    cmd.Parameters["@orderID"].Value = orderIDs;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        countDeleted += 1;
                }

                connection.Close();
            }
            return countDeleted;
        }

        public List<Order> List()
        {
            List<Order> data = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //chuẩn bị câu lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT        Employees.LastName, Customers.CompanyName, Orders.OrderDate, Orders.RequiredDate, Orders.ShippedDate, Orders.Status, Orders.OrderID
                                    FROM            Orders INNER JOIN
                                                             Customers ON Orders.CustomerID = Customers.CustomerID INNER JOIN
                                                             Employees ON Orders.EmployeeID = Employees.EmployeeID
                                    WHERE        (Orders.Status = N'queue')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                //thực thi câu lệnh
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Order()
                        {                         
                            LastName = Convert.ToString(dbReader["LastName"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            OrderDate = Convert.ToDateTime(dbReader["OrderDate"]),
                            RequiredDate = Convert.ToDateTime(dbReader["RequiredDate"]),
                            ShippedDate = Convert.ToDateTime(dbReader["ShippedDate"]),
                            Status = Convert.ToString(dbReader["Status"]),
                            OrderID = Convert.ToInt32(dbReader["OrderID"])
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public List<Order> ListAccepted()
        {
            List<Order> data = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //chuẩn bị câu lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT        Employees.LastName, Customers.CompanyName, Orders.OrderDate, Orders.RequiredDate, Orders.ShippedDate, Orders.Status, Orders.OrderID
                                    FROM            Orders INNER JOIN
                                                             Customers ON Orders.CustomerID = Customers.CustomerID INNER JOIN
                                                             Employees ON Orders.EmployeeID = Employees.EmployeeID
                                    WHERE        (Orders.Status = N'accepted')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                //thực thi câu lệnh
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Order()
                        {
                            LastName = Convert.ToString(dbReader["LastName"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            OrderDate = Convert.ToDateTime(dbReader["OrderDate"]),
                            RequiredDate = Convert.ToDateTime(dbReader["RequiredDate"]),
                            ShippedDate = Convert.ToDateTime(dbReader["ShippedDate"]),
                            Status = Convert.ToString(dbReader["Status"]),
                            OrderID = Convert.ToInt32(dbReader["OrderID"])
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public bool Update(int OrderID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Orders SET Status = 'accepted' WHERE OrderID = @OrderID ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@OrderID", OrderID);
              
                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
