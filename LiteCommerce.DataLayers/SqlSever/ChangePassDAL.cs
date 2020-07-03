using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlSever
{
    public class ChangePassDAL : IChangePassDAL
    {
        private string connectionString;

        public ChangePassDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ChangePass Get(int employeeID)
        {
            ChangePass data = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE EmployeeID = @employeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new ChangePass()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            OldPassWord = Convert.ToString(dbReader["Password"]),
                        };
                    }
                }

                connection.Close();
            }

            return data;
        }
        public bool UpdatePass(ChangePass data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Employees
                                           SET Password = @Password                                                                                          
                                          WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@Password",EncodePass.EncodeMD5(data.NewPassWord));
               
                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }

       
    }
}
