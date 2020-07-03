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
    public class ProductAttributeDAL : IProductAttributeDAL
    {
        private string connectionString;

        public ProductAttributeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ProductAttribute Get(int AttributeID)
        {
            ProductAttribute data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM ProductAttributes WHERE AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@AttributeID", AttributeID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValues = Convert.ToString(dbReader["AttributeValues"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }
        public int Add(ProductAttribute data)
        {
            int attributeId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes
                                          (	                                        
	                                          ProductID,
                                              AttributeName,
                                              AttributeValues,
                                              DisplayOrder	                                          
                                          )
                                          VALUES
                                          (
	                                        
	                                          @ProductID,
                                              @AttributeName,
                                              @AttributeValues,
                                              @DisplayOrder
	                                          
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValues", data.AttributeValues);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);

                attributeId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return attributeId;
        }

        public int Count(string searchValue, int productID)
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
                cmd.CommandText = @"select count(*) from ProductAttributes 
                        where (@searchValue = N'' or AttributeName like @searchValue)
							and ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return count;
        }

        public int Delete(int[] AttributeIDs)
        {
            int countDeleted = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes
                                            WHERE(AttributeID = @attributeID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@attributeID", SqlDbType.Int);
                foreach (int attributeID in AttributeIDs)
                {
                    cmd.Parameters["@attributeID"].Value = attributeID;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        countDeleted += 1;
                }

                connection.Close();
            }
            return countDeleted;
        }

        public List<ProductAttribute> List(int page, int pagesize, string searchValue, int productId)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from (select *, ROW_NUMBER() over(order by AttributeName) as RowNumber
                                    from ProductAttributes
                                    where (ProductID = @productID) and
									  (@searchValue = N'') or (AttributeName like @searchValue) 
                                      )as t
                                    where 
                                        t.RowNumber between(@page-1)*@pageSize + 1 and @page*@pageSize
                                        order by t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pagesize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@productID", productId);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValues = Convert.ToString(dbReader["AttributeValues"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                        });
                    }
                }

                connection.Close();
            }
            return data;
        }

        public bool Update(ProductAttribute data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE ProductAttributes
                                           SET  AttributeName = @AttributeName
                                               ,AttributeValues = @AttributeValues 
                                              ,DisplayOrder = @DisplayOrder                                            
                                          WHERE AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValues", data.AttributeValues);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);


                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
