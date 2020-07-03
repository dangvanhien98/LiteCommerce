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
    public class AttributeDAL : IAttributeDAL
    {
        private string connectionString;

        /// <summary>
        /// construct
        /// </summary>
        /// <param name="connectionString"></param>
        public AttributeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Attributes> List()
        {
            List<Attributes> data = new List<Attributes>();

           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //chuẩn bị câu lệnh
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Attribute";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
               

                //thực thi câu lệnh
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Attributes()
                        {                       
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),                    
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

    }
}
