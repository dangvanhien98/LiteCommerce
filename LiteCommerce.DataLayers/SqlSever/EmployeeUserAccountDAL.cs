using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SqlSever
{
    public class EmployeeUserAccountDAL : IUserAccountDAL
    {
        private string connectionString;
        public EmployeeUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public UserAccount Authorize(string userName, string password)
        {
            //TODO: Kiểm tra thông tin đăng nhập tư bảng employee
            UserAccount data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT EmployeeID, LastName, PhotoPath, Title, Roles FROM Employees WHERE Email = @Email and Password = @Password";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue("@Password", EncodePass.EncodeMD5(password));

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new UserAccount()
                        {
                            UserID = Convert.ToString(dbReader["EmployeeID"]),
                            FullName = Convert.ToString(dbReader["LastName"]),
                            Photo = Convert.ToString(dbReader["PhotoPath"]),
                            Title = Convert.ToString(dbReader["Title"]),
                            Roles = Convert.ToString(dbReader["Roles"])
                        };
                    }
                }

                connection.Close();
            }
            return data;
            //return new UserAccount()
            //{
            //    UserID = userName,
            //    FullName = "Đặng Văn Hiền",
            //    Photo = "5a053a74-a925-4e01-b686-cfb7dac5f8c0avatar.png"
            //};
        }     
    }
}
